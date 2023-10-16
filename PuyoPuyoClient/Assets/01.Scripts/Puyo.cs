using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puyo
{
    //�ڽ��� � �ѿ����� ��Ÿ���� Ÿ��
    [SerializeField]
    private PuyoType _type = PuyoType.None;
    public PuyoType Type => _type;

    //���忡���� �ε���
    private int _posX;
    public int PosX => _posX;

    private int _posY;
    public int PosY => _posY;

    public bool Visited = false;
    public bool IsPop = false;

    //���� �� ȸ�� ��
    public int RotateVal = 0;
    
    /// <summary>
    /// ȸ���� �迭
    /// idx: 0
    /// ������
    /// ������
    /// ������
    /// idx: 1
    /// ������
    /// ������
    /// ������
    /// idx: 2
    /// ������
    /// ������
    /// ������
    /// idx: 3
    /// ������
    /// ������
    /// ������
    /// </summary>
    private Tuple<int, int>[] _rotateVals = 
    { 
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(1, 0), 
        new Tuple<int, int>(0, 1), 
        new Tuple<int, int>(-1, 0) 
    };
    public Tuple<int, int>[] RotateVals => _rotateVals;
    
    //���� ����
    public void Init(PuyoType _type, int _posX, int _posY)
    {
        this._type = _type;
        this._posX = _posX;
        this._posY = _posY;
    }
    
    /// <summary>
    /// �ڽ��� Y �� �Ʒ����� ���� ����� �ѿ並 Ž���ϰ�
    /// �ش� �ѿ��� Y �ε��� -1 �� �ڽ��� Y �ε����� ����
    /// </summary>
    public void Fall()
    {
        GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
        for(int i = _posY + 1; i < 12; ++i)
        {
            if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX, i].Type != PuyoType.None)
            {
                _posY = i - 1;
                break;
            }
            else
            {
                _posY = i;
            }
        }
        GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ʒ��� 1ĭ ����
    public void FallOneBlock()
    {
        if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY + 1].Type == PuyoType.None
            && _posY < 11)
        {
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
            ++_posY;
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        }
    }

    //�Ű������� ���� ��/�� �̵�
    public void Move(int dir)
    {
        if(!(_posX + dir > 5 || _posX + dir < 0) && GameManager.Instance.GetBoard(1).PuyoBoard[_posX + dir, _posY].Type == PuyoType.None)
        {
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
            _posX += dir;
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        }
    }

    //�Ű������� ���� ��/�� ȸ��
    public void RotatePuyo(int dir)
    {
        //���� �ε������� ȸ������ ���� �ش� ��ġ�� �ѿ䰡 �ִ��� Ȯ��
        if(GameManager.Instance.GetBoard(1).PuyoBoard[
            _posX + _rotateVals[(RotateVal + dir) % 4].Item1, 
            _posY + _rotateVals[(RotateVal + dir) % 4].Item2].Type == PuyoType.None)
        {
            RotateVal = (RotateVal + dir) % 4;
            Debug.Log($"{RotateVal}");
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
            _posX = GameManager.Instance.GetBoard(1).CurPuyoPuyo.Puyos[1].PosX + _rotateVals[RotateVal].Item1;
            _posY = GameManager.Instance.GetBoard(1).CurPuyoPuyo.Puyos[1].PosY + _rotateVals[RotateVal].Item2;
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        }
    }

    public void DFS(Puyo puyo)
    {
        Visited = true;
        int x = puyo.PosX;
        int y = puyo.PosY;

        for(int i = 0; i < 4; ++i)
        {
            int nx = x + _rotateVals[i].Item1;
            int ny = y + _rotateVals[i].Item2;

            if (nx < 0 || nx >= 6 || ny < 0 || ny >= 12)
                continue;
            if (GameManager.Instance.GetBoard(1).PuyoBoard[nx, ny].Type == PuyoType.None)
                continue;
            if (GameManager.Instance.GetBoard(1).PuyoBoard[nx, ny].Visited
                || GameManager.Instance.GetBoard(1).PuyoBoard[nx, ny].Type != _type)
                continue;

            DFS(GameManager.Instance.GetBoard(1).PuyoBoard[nx, ny]);
            IsPop = true;
            Pop();
        }
    }

    public void Pop()
    {
        Visited = false;
        IsPop = false;
        _type = PuyoType.None;
        //GameManager.Instance.GetBoard(1).PopPuyoCnt++;
        //if (!(GameManager.Instance.GetBoard(1).ColorBonus.Contains(this._type)))
        //{
        //    GameManager.Instance.GetBoard(1).ColorBonus.Add(this._type);
        //}
    }
}