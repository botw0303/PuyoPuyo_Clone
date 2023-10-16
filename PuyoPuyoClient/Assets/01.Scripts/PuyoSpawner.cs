using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuyoSpawner : MonoBehaviour
{
    //���� �ѿ�ѿ� ��� ����Ʈ
    [SerializeField] private List<PuyoPuyo> _nextPuyoList = new List<PuyoPuyo>();

    public void SpawnStart()
    {
        for (int i = 0; i < 3; ++i)
        {
            PuyoPuyo newPuyoPuyo = new PuyoPuyo();
            _nextPuyoList.Add(newPuyoPuyo);
        }
        _nextPuyoList[0].Init();
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = _nextPuyoList[0];
        FindElements();
    }

    private void Update()
    {
        //���� �ѿ�ѿ䰡 ��������(�� ����� �Űܰ���) ����Ʈ���� ����� ���ο� �ѿ�ѿ� �߰�
    }

    public void MoveNextPuyo()
    {
        _nextPuyoList[1].Init();
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = _nextPuyoList[1];
        _nextPuyoList.RemoveAt(0);
        PuyoPuyo newPuyoPuyo = new PuyoPuyo();
        _nextPuyoList.Add(newPuyoPuyo);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_nextPuyoList[0].Puyos[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_nextPuyoList[0].Puyos[1]);
    }

    private void FindElements()
    {
        
    }
}
