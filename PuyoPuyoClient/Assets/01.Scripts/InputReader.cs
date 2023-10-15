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

        //���� ȭ��ǥ�� ������ ���� - �������� �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Move(-1);
        //������ ȭ��ǥ�� ������ ���� - ���������� �̵�
        else if (Input.GetKey(KeyCode.RightArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Move(1);
        //�Ʒ� ȭ��ǥ�� ������ ���� - ���� �ӵ� ���
        else if (Input.GetKey(KeyCode.DownArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.fallSpeed = 0.3f;
        //�Ʒ� ȭ��ǥ���� ���� �� ���� - ���� �ӵ� ���󺹱�
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.fallSpeed = 1f;

        //Z�� ���� ���� - �������� ȸ��
        if(Input.GetKeyDown(KeyCode.Z))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Rotate(-1);
        //X�� ���� ���� - ���������� ȸ��
        else if (Input.GetKeyDown(KeyCode.X))
            GameManager.Instance.GetBoard(1).CurPuyoPuyo.Rotate(1);
    }
}
