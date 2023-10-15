using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuyoSpawner : MonoBehaviour
{
    private VisualElement _root;

    //���� �ѿ�ѿ� ��� ����Ʈ
    private List<PuyoPuyo> NextPuyoList;
    private List<VisualElement> NextPuyoUIList;

    private void Awake()
    {
        FindElements();
    }

    private void Update()
    {
        //���� �ѿ�ѿ䰡 ��������(�� ����� �Űܰ���) ����Ʈ���� ����� ���ο� �ѿ�ѿ� �߰�
    }

    private void MoveNextPuyo()
    {
        GameManager.Instance.GetBoard(1).CurPuyoPuyo = NextPuyoList[0];
        NextPuyoList.RemoveAt(0);
        PuyoPuyo newPuyoPuyo = new PuyoPuyo();
        NextPuyoList.Add(newPuyoPuyo);
    }

    private void FindElements()
    {
        _root = GameManager.Instance.GetBoard(1).Document.rootVisualElement.Q<VisualElement>("player1-next-puyo");
        NextPuyoUIList = _root.Query<VisualElement>("player1-next-puyo").ToList();
    }
}
