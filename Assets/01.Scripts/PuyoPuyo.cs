using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo : MonoBehaviour
{
    //�ѿ� ���
    private Puyo[] _puyopuyo = new Puyo[2];

    //��� �ð�, ���� �ӵ�
    public float interval = 0f;
    public float fallSpeed = 0.5f;

    public bool IsLanding = false;

    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GameObject.Find("Player1").GetComponent<InputReader>();

        //���� ��ġ ����
        _puyopuyo[0].Init((PuyoType)Random.Range(1, 6), 2, 0);
        _puyopuyo[1].Init((PuyoType)Random.Range(1, 6), 2, 1);

        //�ѿ� ����Ʈ�� �߰�
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[0]);
        GameManager.Instance.GetBoard(1).PuyoList.Add(_puyopuyo[1]);

        //�̺�Ʈ ����
        _inputReader.OnRotateKeyPress += Rotate;
    }

    //����
    public void Fall()
    {
        _puyopuyo[0].FallOneBlock();
        _puyopuyo[1].FallOneBlock();
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ű������� ���� ���� ȸ��
    public void Move(int dir)
    {
        _puyopuyo[0].Move(dir);
        _puyopuyo[1].Move(dir);
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    //�Ű������� ���� ���� ȸ��
    public void Rotate(int dir)
    {
        _puyopuyo[1].RotatePuyo(dir);
        GameManager.Instance.GetBoard(1).BoardRender();
    }

    public void DeSub()
    {
        _inputReader.OnRotateKeyPress -= Rotate;
    }
}
