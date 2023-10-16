using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuyoPuyo
{
    //�ѿ� ���
    [SerializeField]
    private Puyo[] _puyopuyo = new Puyo[2];
    public Puyo[] Puyos => _puyopuyo;

    //��� �ð�, ���� �ӵ�
    public float interval = 0f;
    public float fallSpeed = 0.5f;

    public bool IsLanding = false;

    private InputReader _inputReader;

    public void Setting()
    {
        _inputReader = GameObject.Find("Player1").GetComponent<InputReader>();

        //���� ��ġ ����
        Puyo newPuyo1 = new Puyo();
        newPuyo1.Init((PuyoType)Random.Range(1, 6), 2, 0);
        _puyopuyo[0] = newPuyo1;
        Puyo newPuyo2 = new Puyo();
        newPuyo2.Init((PuyoType)Random.Range(1, 6), 2, 1);
        _puyopuyo[1] = newPuyo2;
    }

    public void Init()
    {
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
        //Debug.Log($"puyopuyo0 {_puyopuyo[0].PosX}, {_puyopuyo[0].PosY}");
        //Debug.Log($"puyopuyo1 {_puyopuyo[1].PosX}, {_puyopuyo[1].PosY}");
        if(_puyopuyo[0].PosY < 11 && _puyopuyo[1].PosY < 11)
        {

            if(_puyopuyo[0].PosY > _puyopuyo[1].PosY)
            {
                if((GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[0].PosX, _puyopuyo[0].PosY + 1].Type == PuyoType.None
                    && GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[1].PosX, _puyopuyo[1].PosY + 1].Type == PuyoType.None)
                    || (GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[0].PosX, _puyopuyo[0].PosY + 1].Type != PuyoType.None
                    || GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[1].PosX, _puyopuyo[1].PosY + 1].Type != PuyoType.None)
                    && (_puyopuyo[0].RotateVal == 0 || _puyopuyo[0].RotateVal == 2))
                {
                    _puyopuyo[0].FallOneBlock();
                    _puyopuyo[1].FallOneBlock();
                }
                else
                {
                    _puyopuyo[0].Fall();
                    _puyopuyo[1].Fall();
                    GameManager.Instance.GetSpawner().MoveNextPuyo();
                }
            }
            else
            {
                //if ((GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[0].PosX, _puyopuyo[0].PosY + 1].Type == PuyoType.None
                //    && GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[1].PosX, _puyopuyo[1].PosY + 1].Type == PuyoType.None)
                //    || ((GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[0].PosX, _puyopuyo[0].PosY + 1].Type != PuyoType.None
                //    || GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[1].PosX, _puyopuyo[1].PosY + 1].Type != PuyoType.None)
                //    && (_puyopuyo[0].RotateVal == 0 || _puyopuyo[0].RotateVal == 2))
                //    || ((GameManager.Instance.GetBoard(1).PuyoBoard[_puyopuyo[0].PosX, _puyopuyo[0].PosY + 1].Type == PuyoType.None)
                //{
                //    _puyopuyo[1].FallOneBlock();
                //    _puyopuyo[0].FallOneBlock();
                //}
                //else
                //{
                //    _puyopuyo[1].Fall();
                //    _puyopuyo[0].Fall();
                //    GameManager.Instance.GetSpawner().MoveNextPuyo();
                //}
            }
        }
        else
        {
            if (_puyopuyo[0].PosY > _puyopuyo[1].PosY)
            {
                _puyopuyo[0].Fall();
                _puyopuyo[1].Fall();
            }
            else
            {
                _puyopuyo[1].Fall();
                _puyopuyo[0].Fall();
            }
            GameManager.Instance.GetSpawner().MoveNextPuyo();
        }
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ű������� ���� ���� ȸ��
    public void Move(int dir)
    {
        if(dir == 1)
        {
            if(_puyopuyo[0].PosX > _puyopuyo[1].PosX)
            {
                _puyopuyo[0].Move(dir);
                _puyopuyo[1].Move(dir);
            }
            else
            {
                _puyopuyo[1].Move(dir);
                _puyopuyo[0].Move(dir);
            }
        }
        else
        {
            if (_puyopuyo[0].PosX > _puyopuyo[1].PosX)
            {
                _puyopuyo[1].Move(dir);
                _puyopuyo[0].Move(dir);
            }
            else
            {
                _puyopuyo[0].Move(dir);
                _puyopuyo[1].Move(dir);
            }
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
