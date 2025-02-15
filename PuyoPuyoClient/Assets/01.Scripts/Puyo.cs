using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puyo
{
    //切重戚 嬢恐 姿推昔走 蟹展鎧澗 展脊
    [SerializeField]
    private PuyoType _type = PuyoType.None;
    public PuyoType Type => _type;

    //左球拭辞税 昔畿什
    private int _posX;
    public int PosX => _posX;

    private int _posY;
    public int PosY => _posY;

    public bool Visited = false;
    public bool IsPop = false;

    //薄仙 鎧 噺穿 葵
    public int RotateVal = 0;
    
    /// <summary>
    /// 噺穿葵 壕伸
    /// idx: 0
    /// だしだ
    /// だしだ
    /// だだだ
    /// idx: 1
    /// だだだ
    /// だしし
    /// だだだ
    /// idx: 2
    /// だだだ
    /// だしだ
    /// だしだ
    /// idx: 3
    /// だだだ
    /// ししだ
    /// だだだ
    /// </summary>
    private Tuple<int, int>[] _rotateVals = 
    { 
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(1, 0), 
        new Tuple<int, int>(0, 1), 
        new Tuple<int, int>(-1, 0) 
    };
    public Tuple<int, int>[] RotateVals => _rotateVals;
    
    //痕呪 室特
    public void Init(PuyoType _type, int _posX, int _posY)
    {
        this._type = _type;
        this._posX = _posX;
        this._posY = _posY;
    }
    
    /// <summary>
    /// 切重税 Y 逐 焼掘拭辞 亜舌 亜猿錘 姿推研 貼事馬壱
    /// 背雁 姿推税 Y 昔畿什 -1 聖 切重税 Y 昔畿什稽 竺舛
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

    //焼掘稽 1牒 開馬
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

    //古鯵痕呪拭 魚虞 疎/酔 戚疑
    public void Move(int dir)
    {
        if(!(_posX + dir > 5 || _posX + dir < 0) && GameManager.Instance.GetBoard(1).PuyoBoard[_posX + dir, _posY].Type == PuyoType.None)
        {
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = new Puyo();
            _posX += dir;
            GameManager.Instance.GetBoard(1).PuyoBoard[_posX, _posY] = this;
        }
    }

    //古鯵痕呪拭 魚虞 疎/酔 噺穿
    public void RotatePuyo(int dir)
    {
        //薄仙 昔畿什拭辞 噺穿葵聖 希背 背雁 是帖拭 姿推亜 赤澗走 溌昔
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
        Debug.Log($"x: {x}, y: {y}");

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

            GameManager.Instance.GetBoard(1).PopPuyoCnt++;
            if(!GameManager.Instance.GetBoard(1).ColorBonus.ContainsKey(_type))
                GameManager.Instance.GetBoard(1).ColorBonus.Add(_type, _type);
            GameManager.Instance.GetBoard(1).ConnectBonus++;
            IsPop = true;
            DFS(GameManager.Instance.GetBoard(1).PuyoBoard[nx, ny]);
            if(GameManager.Instance.GetBoard(1).PopPuyoCnt <= 3)
            {
                GameManager.Instance.GetBoard(1).PopPuyoCnt--;
                GameManager.Instance.GetBoard(1).ColorBonus.Remove(_type);
                GameManager.Instance.GetBoard(1).ConnectBonus--;
                IsPop = false;
            }
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