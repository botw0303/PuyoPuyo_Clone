using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    //현재 보드에 있는 뿌요 리스트
    public List<Puyo> PuyoList = new List<Puyo>();

    
    
    /// <summary>
    /// 현재 보드 배치
    /// 저장과 렌더링, 탐색용
    /// 현재는 뿌요가 움직일 때 원래 위치에 임시 객체를 넣고 자신이 이동한 위치에 자신을 넣어주는 형식으로
    /// 이걸 1초마다 반복 중임. 내일 도윤이한테 구조 물어볼 예정
    /// </summary>
    public Puyo[,] PuyoBoard = new Puyo[6, 12];

    //UI
    //[SerializeField]
    //private VisualTreeAsset _treeAsset;
    [SerializeField] private UIDocument document;
    public UIDocument Document => document;
    private VisualElement _root;
    public VisualElement[,] RenderBoard = new VisualElement[6, 12];

    //현재 낙하하고 있는 뿌요뿌요
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

            //점수 구하는 거 하는 도중이었음(지금은 연쇄 보너스 작업 중)
            //ChainBonus

            for(int i = 0; i < PuyoList.Count; ++i)
            {
                PuyoList[i].Fall();
            }

            BoardRender();

            /*
            아래로 한칸씩 떨어지고
            좌우로 움직일 수 있고
            떨어지는 거 가속할 수 있고
            회전시킬 수 있고
            착지하면 주변 확인해서 터짐
            다 떨어지면 아래로 더 갈 수 있는지 확인하고 떨어져야하면 떨어지게 해야함
            한번 터트리고 뿌요 다 떨어지게하고 다시 확인해서 터트릴 거 있는지 확인해야함
            
            터진 수, 연쇄 수 등에 따라서 점수 줘야함
            점수에 따라서 상대한테 방해뿌요 떨궈줘야함
            뿌요, 점수, 승점, 다음 뿌요 렌더링
            패배 조건 체크해줘야함
            연출(이펙트, 사운드)
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
