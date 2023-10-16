using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuyoSpawner : MonoBehaviour
{
    private VisualElement _root;

    //���� �ѿ�ѿ� ��� ����Ʈ
    private List<PuyoPuyo> _nextPuyoList;
    private List<VisualElement> _nextPuyoUIList;

    private void Awake()
    {
        for(int i = 0; i < 3; ++i)
        {
            PuyoPuyo newPuyoPuyo = new PuyoPuyo();
            _nextPuyoList.Add(newPuyoPuyo);
        }
        _nextPuyoList[0].Init();
        FindElements();
    }

    private void Update()
    {
        //���� �ѿ�ѿ䰡 ��������(�� ����� �Űܰ���) ����Ʈ���� ����� ���ο� �ѿ�ѿ� �߰�
    }

    public void MoveNextPuyo()
    {
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = _nextPuyoList[1];
        _nextPuyoList.RemoveAt(0);
        PuyoPuyo newPuyoPuyo = new PuyoPuyo();
        _nextPuyoList.Add(newPuyoPuyo);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_nextPuyoList[0].Puyos[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_nextPuyoList[0].Puyos[1]);
    }

    private void FindElements()
    {
        _root = GameManager.Instance.GetBoard(1).Document.rootVisualElement.Q<VisualElement>("player1-next-puyo");
        _nextPuyoUIList = _root.Query<VisualElement>("player1-next-puyo").ToList();
    }
}
