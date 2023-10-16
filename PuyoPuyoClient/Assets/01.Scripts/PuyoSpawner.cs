using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuyoSpawner : MonoBehaviour
{
    //다음 뿌요뿌요 대기 리스트
    public List<PuyoPuyo> NextPuyoList = new List<PuyoPuyo>();

    public void SpawnStart()
    {
        for (int i = 0; i < 3; ++i)
        {
            PuyoPuyo newPuyoPuyo = new PuyoPuyo();
            newPuyoPuyo.Setting();
            NextPuyoList.Add(newPuyoPuyo);
        }
        NextPuyoList[0].Init();
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = NextPuyoList[0];
    }

    public void MoveNextPuyo()
    {
        NextPuyoList[1].Init();
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = NextPuyoList[1];
        NextPuyoList.RemoveAt(0);
        PuyoPuyo newPuyoPuyo = new PuyoPuyo();
        newPuyoPuyo.Setting();
        NextPuyoList.Add(newPuyoPuyo);
        GameManager.Instance.GetBoard(1).PuyoList.Add(NextPuyoList[0].Puyos[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(NextPuyoList[0].Puyos[1]);
    }
}
