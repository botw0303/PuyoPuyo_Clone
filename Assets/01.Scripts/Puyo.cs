using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    //�ڽ��� � �ѿ����� ��Ÿ���� Ÿ��
    private PuyoType _type = PuyoType.None;
    public PuyoType Type => _type;

    //���忡���� �ε���
    private int _posX;
    public int PosX => _posX;

    private int _posY;
    public int PosY => _posY;

    public int MinPosX;
    public int MaxPosX;
    public int MinPosY;
    public int MaxPosY;

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
        new Tuple<int, int>(0, -1), 
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
        for(int i = _posY; i < 12; ++i)
        {
            if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX, i].Type != PuyoType.None)
            {
                _posY = i - 1;
                break;
            }
        }
        GameManager.Instance.GetBoard(1).BoardRender();
        GameManager.Instance.GetBoard(1).CurPuyoPuyo.DeSub();
    }

    //�Ʒ��� 1ĭ ����
    public void FallOneBlock()
    {
        if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY - 1].Type == PuyoType.None
            && _posY < 11)
        {
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
            ++_posY;
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        }
        else
        {
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.IsLanding = true;
            
        }
    }

    //�Ű������� ���� ��/�� �̵�
    public void Move(int dir)
    {
        if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX + dir, _posY].Type == PuyoType.None
            && !(_posX + dir > 5 || _posX + dir < 0))
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
        if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX + _rotateVals[(RotateVal + dir) % 4].Item1, _posY + _rotateVals[(RotateVal + dir) % 4].Item2].Type == PuyoType.None)
        {
            RotateVal = (RotateVal + dir) % 4;
        }
    }
}