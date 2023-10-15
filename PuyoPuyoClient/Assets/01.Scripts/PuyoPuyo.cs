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
        Puyo newPuyo1 = new Puyo();
        newPuyo1.Init((PuyoType)Random.Range(1, 6), 2, 0);
        Puyo newPuyo2 = new Puyo();
        newPuyo2.Init((PuyoType)Random.Range(1, 6), 2, 1);
        _puyopuyo[0] = newPuyo1;
        _puyopuyo[1] = newPuyo2;

        //뿌요 리스트에 추가
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[1]);

        //이벤트 구독
        _inputReader.OnRotateKeyPress += Rotate;
    }

    //낙하
    public void Fall()
    {
        if(_puyopuyo[0].PosY < 11 || _puyopuyo[1].PosY < 11)
        {
            _puyopuyo[0].FallOneBlock();
            _puyopuyo[1].FallOneBlock();
            GameManager.Instance.GetBoard(1).BoardRender();
        }
        else
        {
            IsLanding = true;
            _puyopuyo[0].Fall();
            _puyopuyo[1].Fall();
            //GameManager.Instance.GetBoard(1).NextPuyoList.RemoveAt(0);
            //GameManager.Instance.GetBoard(1).CurPuyoPuyo = null;
            //GameManager.Instance.GetBoard(1).CurPuyoPuyo = GameManager.Instance.GetBoard(1).NextPuyoList[0];
            //GameManager.Instance.GetBoard(1).BoardRender();
        }
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
        //GameManager.Instance.GetBoard(1).NextPuyoList.RemoveAt(0);
    }
}
