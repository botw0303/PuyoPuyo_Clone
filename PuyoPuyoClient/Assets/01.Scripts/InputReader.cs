using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<int> OnRotateKeyPress = null;

    private void Update()
    {
        //Test Key
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("isClicked Q");
            PuyoPuyo newPuyoPuyo = new PuyoPuyo();
            GameManager.Instance.GetBoard(1).CurPuyoPuyo = newPuyoPuyo;
        }

        //왼쪽 화살표를 누르는 동안 - 왼쪽으로 이동
        if (Input.GetKey(KeyCode.LeftArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Move(-1);
        //오른쪽 화살표를 누르는 동안 - 오른쪽으로 이동
        else if (Input.GetKey(KeyCode.RightArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Move(1);
        //아래 화살표를 누르는 동안 - 낙하 속도 상승
        else if (Input.GetKey(KeyCode.DownArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.fallSpeed = 0.3f;
        //아래 화살표에서 손을 땐 시점 - 낙하 속도 원상복귀
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.fallSpeed = 1f;

        //Z를 누른 시점 - 왼쪽으로 회전
        if(Input.GetKeyDown(KeyCode.Z))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Rotate(-1);
        //X를 누른 시점 - 오른쪽으로 회전
        else if (Input.GetKeyDown(KeyCode.X))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Rotate(1);
    }
}
