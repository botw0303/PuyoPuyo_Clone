using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;

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

    private List<Tuple<VisualElement, VisualElement>> _nextPuyoUIList = new List<Tuple<VisualElement, VisualElement>>();

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
        GameManager.Instance.GetSpawner().SpawnStart();
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
            }
            FindSamePuyo();
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
        PuyoList.ForEach(puyo =>
        {
            puyo.DFS(puyo);
        });
        //while (_isCalc)
        //{
        //    PuyoList.ForEach(puyo =>
        //    {
        //        puyo.DFS(puyo);
        //    });

        //    _isCalc = false;

        //    for(int i = 0; i < PuyoList.Count; ++i)
        //    {
        //        if (PuyoList[i].IsPop)
        //        {
        //            _isCalc = true;
        //            PuyoList[i].Pop();
        //            PuyoList.RemoveAt(i);
        //        }
        //    }

        //    //���� ���ϴ� �� �ϴ� �����̾���(������ ���� ���ʽ� �۾� ��)
        //    //ChainBonus

        //    for(int i = 0; i < PuyoList.Count; ++i)
        //    {
        //        PuyoList[i].Fall();
        //    }

        //    BoardRender();

        //    /*
        //    �Ʒ��� ��ĭ�� ��������
        //    �¿�� ������ �� �ְ�
        //    �������� �� ������ �� �ְ�
        //    ȸ����ų �� �ְ�
        //    �����ϸ� �ֺ� Ȯ���ؼ� ����
        //    �� �������� �Ʒ��� �� �� �� �ִ��� Ȯ���ϰ� ���������ϸ� �������� �ؾ���
        //    �ѹ� ��Ʈ���� �ѿ� �� ���������ϰ� �ٽ� Ȯ���ؼ� ��Ʈ�� �� �ִ��� Ȯ���ؾ���
            
        //    ���� ��, ���� �� � ���� ���� �����
        //    ������ ���� ������� ���ػѿ� ���������
        //    �ѿ�, ����, ����, ���� �ѿ� ������
        //    �й� ���� üũ�������
        //    ����(����Ʈ, ����)
        //     */
        //}
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

        for(int i = 0; i < 2; ++i)
        {
            //Debug.Log(_nextPuyoUIList[i].Item1.style.backgroundImage == null);
            //Debug.Log(_nextPuyoUIList[i].Item2.style.backgroundImage == null);
            
            //Debug.Log(GameManager.Instance.GetSpawner().NextPuyoList[i + 1].Puyos[0] == null);
            //Debug.Log(GameManager.Instance.GetSpawner().NextPuyoList[i + 1].Puyos[1] == null);
            _nextPuyoUIList[i].Item1.style.backgroundImage =
            new StyleBackground(Resources.Load<Sprite>(
                $"Puyo Resources/{GameManager.Instance.GetSpawner().NextPuyoList[i + 1].Puyos[0].Type} Puyo (S)"));
            _nextPuyoUIList[i].Item2.style.backgroundImage =
                new StyleBackground(Resources.Load<Sprite>(
                    $"Puyo Resources/{GameManager.Instance.GetSpawner().NextPuyoList[i + 1].Puyos[1].Type} Puyo (S)"));
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

        VisualElement nextPuyoContents = _root.Q<VisualElement>("player1-next-puyo-contents");
        for (int i = 0; i < 2; ++i)
        {
            VisualElement item1 = new VisualElement();
            item1 = nextPuyoContents[i][0];
            VisualElement item2 = new VisualElement();
            item2 = nextPuyoContents[i][1];
            _nextPuyoUIList.Add(new Tuple<VisualElement, VisualElement>(item1, item2));
        }
    }
}
