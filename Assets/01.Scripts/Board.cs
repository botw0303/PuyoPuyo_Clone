using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    //���� ���忡 �ִ� �ѿ� ����Ʈ
    public List<Puyo> PuyoList;
    
    /// <summary>
    /// ���� ���� ��ġ
    /// ����� ������, Ž����
    /// ����� �ѿ䰡 ������ �� ���� ��ġ�� �ӽ� ��ü�� �ְ� �ڽ��� �̵��� ��ġ�� �ڽ��� �־��ִ� ��������
    /// �̰� 1�ʸ��� �ݺ� ����. ���� ���������� ���� ��� ����
    /// </summary>
    public Puyo[,] PuyoBoard = new Puyo[6, 12];

    //UI
    [SerializeField]
    private VisualTreeAsset _treeAsset;
    private VisualElement _root;
    public VisualElement[,] RenderBoard = new VisualElement[6, 12];

    //���� �����ϰ� �ִ� �ѿ�ѿ�
    public PuyoPuyo CurPuyoPuyo;

    public float interval = 0f;
    public float fallSpeed = 0.5f;

    private bool _isCalc = false;

    private void Awake()
    {
        _root = _treeAsset.Instantiate();
        _root = _root.Q<VisualElement>("player1-board");
        FindElements();
    }

    public void Update()
    {
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        if (interval > fallSpeed)
        {
            CurPuyoPuyo.Fall();
            FindSamePuyo();
            BoardRender();
            interval = 0f;
        }
        else
        {
            interval += Time.deltaTime;
        }
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


        }
    }

    public void BoardRender()
    {
        for(int i = 0; i < 6; ++i)
        {
            for(int j = 0; j < 12; ++j)
            {
                RenderBoard[i, j].style.backgroundImage = new StyleBackground(Resources.Load<Sprite>($"{PuyoBoard[i, j].Type} Puyo (S)"));
            }
        }
    }

    private void FindElements()
    {
        List<VisualElement> tiles = _root.Query<VisualElement>(className: "tile").ToList();

        int k = 0;
        for(int i = 0; i < 6; ++i)
        {
            for(int j = 0; j < 12; ++j)
            {
                RenderBoard[i, j] = tiles[k];
                ++k;
            }
        }
    }
}
