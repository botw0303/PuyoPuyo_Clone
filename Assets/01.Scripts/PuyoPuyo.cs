using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo : MonoBehaviour
{
    //뿌요 덩어리
    private Puyo[] _puyopuyo = new Puyo[2];

    //경과 시간, 낙하 속도
    public float interval = 0f;
    public float fallSpeed = 0.5f;

    public bool IsLanding = false;

    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GameObject.Find("Player1").GetComponent<InputReader>();

        //시작 위치 세팅
        _puyopuyo[0].Init((PuyoType)Random.Range(1, 6), 2, 0);
        _puyopuyo[1].Init((PuyoType)Random.Range(1, 6), 2, 1);

        //뿌요 리스트에 추가
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[1]);

        //이벤트 구독
        _inputReader.OnRotateKeyPress += Rotate;
    }

    //낙하
    public void Fall()
    {
        _puyopuyo[0].FallOneBlock();
        _puyopuyo[1].FallOneBlock();
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //매개변수의 값에 따라 회전
    public void Move(int dir)
    {
        _puyopuyo[0].Move(dir);
        _puyopuyo[1].Move(dir);
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //매개변수의 값에 따라 회전
    public void Rotate(int dir)
    {
        _puyopuyo[1].RotatePuyo(dir);
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    public void DeSub()
    {
        _inputReader.OnRotateKeyPress -= Rotate;
    }
}
