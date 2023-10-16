using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    //�ѿ� ���
    private Puyo[] _puyopuyo = new Puyo[2];
    public Puyo[] Puyos => _puyopuyo;

    //��� �ð�, ���� �ӵ�
    public float interval = 0f;
    public float fallSpeed = 0.5f;

    public bool IsLanding = false;

    private InputReader _inputReader;

    public void Init()
    {
        _inputReader = GameObject.Find("Player1").GetComponent<InputReader>();

        //���� ��ġ ����
        Puyo newPuyo1 = new Puyo();
        newPuyo1.Init((PuyoType)Random.Range(1, 6), 2, 0);
        _puyopuyo[0] = newPuyo1;
        Puyo newPuyo2 = new Puyo();
        newPuyo2.Init((PuyoType)Random.Range(1, 6), 2, 1);
        _puyopuyo[1] = newPuyo2;

        //�ѿ� ����Ʈ�� �߰�
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[1]);

        //�̺�Ʈ ����
        _inputReader.OnRotateKeyPress += Rotate;

        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //����
    public void Fall()
    {
        if(_puyopuyo[0].PosY < 11 && _puyopuyo[1].PosY < 11)
        {
            if(_puyopuyo[0].PosY > _puyopuyo[1].PosY)
            {
                _puyopuyo[0].FallOneBlock();
                _puyopuyo[1].FallOneBlock();
            }
            else
            {
                _puyopuyo[1].FallOneBlock();
                _puyopuyo[0].FallOneBlock();
            }
        }
        else
        {
            IsLanding = true;
            _puyopuyo[1].Fall();
            _puyopuyo[0].Fall();
            //GameManager.Instance.GetBoard(1).NextPuyoList.RemoveAt(0);
            //GameManager.Instance.GetBoard(1).CurPuyoPuyo = null;
            //GameManager.Instance.GetBoard(1).CurPuyoPuyo = GameManager.Instance.GetBoard(1).NextPuyoList[0];
            //GameManager.Instance.GetBoard(1).BoardRender();
        }
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ű������� ���� ���� ȸ��
    public void Move(int dir)
    {
        if(_puyopuyo[0].PosX > _puyopuyo[1].PosX)
        {
            _puyopuyo[1].Move(dir);
            _puyopuyo[0].Move(dir);
        }
        else
        {
            _puyopuyo[0].Move(dir);
            _puyopuyo[1].Move(dir);
        }
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ű������� ���� ���� ȸ��
    public void Rotate(int dir)
    {
        _puyopuyo[0].RotatePuyo(dir);
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    public void DeSub()
    {
        _inputReader.OnRotateKeyPress -= Rotate;
        //GameManager.Instance.GetBoard(1).NextPuyoList.RemoveAt(0);
    }
}
