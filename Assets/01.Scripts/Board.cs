using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    //현재 보드에 있는 뿌요 리스트
    public List<Puyo> PuyoList;
    
    /// <summary>
    /// 현재 보드 배치
    /// 저장과 렌더링, 탐색용
    /// 현재는 뿌요가 움직일 때 원래 위치에 임시 객체를 넣고 자신이 이동한 위치에 자신을 넣어주는 형식으로
    /// 이걸 1초마다 반복 중임. 내일 도윤이한테 구조 물어볼 예정
    /// </summary>
    public Puyo[,] PuyoBoard = new Puyo[6, 12];

    //UI
    [SerializeField]
    private VisualTreeAsset _treeAsset;
    private VisualElement _root;
    public VisualElement[,] RenderBoard = new VisualElement[6, 12];

    //현재 낙하하고 있는 뿌요뿌요
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
