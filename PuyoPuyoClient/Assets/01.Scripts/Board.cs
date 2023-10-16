using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    //���� ���忡 �ִ� �ѿ� ����Ʈ
    public List<Puyo> PuyoList = new List<Puyo>();

    
    
    /// <summary>
    /// ���� ���� ��ġ
    /// ����� ������, Ž����
    /// ����� �ѿ䰡 ������ �� ���� ��ġ�� �ӽ� ��ü�� �ְ� �ڽ��� �̵��� ��ġ�� �ڽ��� �־��ִ� ��������
    /// �̰� 1�ʸ��� �ݺ� ����. ���� ���������� ���� ��� ����
    /// </summary>
    public Puyo[,] PuyoBoard = new Puyo[6, 12];

    //UI
    //[SerializeField]
    //private VisualTreeAsset _treeAsset;
    [SerializeField] private UIDocument document;
    public UIDocument Document => document;
    private VisualElement _root;
    public VisualElement[,] RenderBoard = new VisualElement[6, 12];

    //���� �����ϰ� �ִ� �ѿ�ѿ�
    public PuyoPuyo CurPuyoPuyo;

    public float interval = 0f;
    public float fallSpeed = 0.5f;

    private bool _isCalc = false;

    private int PopPuyoCnt = 0;
    private int ChainBonus = 0;
    private int ConnectBonus = 0;
    private List<PuyoType> ColorBonus = new List<PuyoType>();

    public bool IsGameOver = false;

    private List<VisualElement> _nextPuyoUIList = new List<VisualElement>();

    private void Start()
    {
        for(int i = 0; i < 12; ++i)
        {
            for(int j = 0; j < 6; ++j)
            {
                PuyoBoard[j, i] = new Puyo();
                PuyoBoard[j, i].Init(PuyoType.None, j, i);
            }
        }

        //_root = _treeAsset.Instantiate();
        _root = document.rootVisualElement.Q<VisualElement>("player1-board");
        FindElements();
        GameManager.Instance.GetSpanwer().SpawnStart();
        BoardRender();
    }

    public void Update()
    {
        if(CurPuyoPuyo != null)
        {
            for(int i = 0; i < 2; i++)
            {
                //Debug.Log($"puyo0 PosX: {CurPuyoPuyo.Puyos[0].PosX}, PosY: {CurPuyoPuyo.Puyos[0].PosY} puyo1 PosX: {CurPuyoPuyo.Puyos[1].PosX}, PosY: {CurPuyoPuyo.Puyos[1].PosY}");
                //Debug.Log($"puyo0 PosX: {CurPuyoPuyo.Puyos[0].PosX}, PosY: {CurPuyoPuyo.Puyos[0].PosY}");
                //Debug.Log($"puyo1 PosX: {CurPuyoPuyo.Puyos[1].PosX}, PosY: {CurPuyoPuyo.Puyos[1].PosY}");
            }
        }
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        if (interval > fallSpeed)
        {
            if(CurPuyoPuyo != null)
            {
                CurPuyoPuyo.Fall();
                Debug.Log("_curPuyoPuyo is not null");
            }
            //FindSamePuyo();
            interval = 0f;
        }
        else
        {
            interval += Time.deltaTime;
        }
        BoardRender();
    }

    public void FindSamePuyo()
    {
        while (_isCalc)
        {
            PuyoList.ForEach(puyo =>
            {
                puyo.DFS(puyo.PosX, puyo.PosY);
            });

            _isCalc = false;

            for(int i = 0; i < PuyoList.Count; ++i)
            {
                if (PuyoList[i].IsPop)
                {
                    _isCalc = true;
                    PuyoList[i].Pop();
                    PuyoList.RemoveAt(i);
                }
            }

            //���� ���ϴ� �� �ϴ� �����̾���(������ ���� ���ʽ� �۾� ��)
            //ChainBonus

            for(int i = 0; i < PuyoList.Count; ++i)
            {
                PuyoList[i].Fall();
            }

            BoardRender();

            /*
            �Ʒ��� ��ĭ�� ��������
            �¿�� ������ �� �ְ�
            �������� �� ������ �� �ְ�
            ȸ����ų �� �ְ�
            �����ϸ� �ֺ� Ȯ���ؼ� ����
            �� �������� �Ʒ��� �� �� �� �ִ��� Ȯ���ϰ� ���������ϸ� �������� �ؾ���
            �ѹ� ��Ʈ���� �ѿ� �� ���������ϰ� �ٽ� Ȯ���ؼ� ��Ʈ�� �� �ִ��� Ȯ���ؾ���
            
            ���� ��, ���� �� � ���� ���� �����
            ������ ���� ������� ���ػѿ� ���������
            �ѿ�, ����, ����, ���� �ѿ� ������
            �й� ���� üũ�������
            ����(����Ʈ, ����)
             */
        }
    }

    public void BoardRender()
    {
        for(int i = 0; i < 12; ++i)
        {
            for(int j = 0; j < 6; ++j)
            {
                Sprite sprite = Resources.Load<Sprite>($"Puyo Resources/{PuyoBoard[j, i].Type} Puyo (S)");
                StyleBackground style = new StyleBackground(sprite);
                RenderBoard[j, i].style.backgroundImage = style;
            }
        }
    }

    private void FindElements()
    {
        List<VisualElement> tiles = _root.Query<VisualElement>(className: "tile").ToList();

        int k = 0;
        for(int i = 0; i < 12; ++i)
        {
            for(int j = 0; j < 6; ++j)
            {
                RenderBoard[j, i] = tiles[k];
                ++k;
            }
        }

        _root = GameManager.Instance.GetBoard(1).Document.rootVisualElement.Q<VisualElement>("player1-next-puyo");
        _nextPuyoUIList = _root.Query<VisualElement>("player1-next-puyo").ToList();
    }
}
