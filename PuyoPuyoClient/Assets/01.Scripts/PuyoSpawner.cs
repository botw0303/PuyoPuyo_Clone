using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuyoSpawner : MonoBehaviour
{
    private VisualElement _root;

    //다음 뿌요뿌요 대기 리스트
    private List<PuyoPuyo> NextPuyoList;
    private List<VisualElement> NextPuyoUIList;

    private void Awake()
    {
        FindElements();
    }

    private void Update()
    {
        //현재 뿌요뿌요가 없어지면(즉 보드로 옮겨가면) 리스트에서 지우고 새로운 뿌요뿌요 추가
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
