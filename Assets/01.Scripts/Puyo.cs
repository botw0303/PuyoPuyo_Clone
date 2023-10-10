using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    //자신이 어떤 뿌요인지 나타내는 타입
    private PuyoType _type = PuyoType.None;
    public PuyoType Type => _type;

    //보드에서의 인덱스
    private int _posX;
    public int PosX => _posX;

    private int _posY;
    public int PosY => _posY;

    public int MinPosX;
    public int MaxPosX;
    public int MinPosY;
    public int MaxPosY;

    //현재 내 회전 값
    public int RotateVal = 0;
    
    /// <summary>
    /// 회전값 배열
    /// idx: 0
    /// ㅐㅇㅐ
    /// ㅐㅇㅐ
    /// ㅐㅐㅐ
    /// idx: 1
    /// ㅐㅐㅐ
    /// ㅐㅇㅇ
    /// ㅐㅐㅐ
    /// idx: 2
    /// ㅐㅐㅐ
    /// ㅐㅇㅐ
    /// ㅐㅇㅐ
    /// idx: 3
    /// ㅐㅐㅐ
    /// ㅇㅇㅐ
    /// ㅐㅐㅐ
    /// </summary>
    private Tuple<int, int>[] _rotateVals = 
    { 
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(1, 0), 
        new Tuple<int, int>(0, -1), 
        new Tuple<int, int>(-1, 0) 
    };
    public Tuple<int, int>[] RotateVals => _rotateVals;
    
    //변수 세팅
    public void Init(PuyoType _type, int _posX, int _posY)
    {
        this._type = _type;
        this._posX = _posX;
        this._posY = _posY;
    }
    
    /// <summary>
    /// 자신의 Y 축 아래에서 가장 가까운 뿌요를 탐색하고
    /// 해당 뿌요의 Y 인덱스 -1 을 자신의 Y 인덱스로 설정
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

    //아래로 1칸 낙하
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

    //매개변수에 따라 좌/우 이동
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

    //매개변수에 따라 좌/우 회전
    public void RotatePuyo(int dir)
    {
        //현재 인덱스에서 회전값을 더해 해당 위치에 뿌요가 있는지 확인
        if(GameManager.Instance.GetBoard(1).PuyoBoard[_posX + _rotateVals[(RotateVal + dir) % 4].Item1, _posY + _rotateVals[(RotateVal + dir) % 4].Item2].Type == PuyoType.None)
        {
            RotateVal = (RotateVal + dir) % 4;
        }
    }
}