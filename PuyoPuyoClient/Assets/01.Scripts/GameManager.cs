using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    //�÷��̾�1, 2�� ����
    private Board[] _boards = new Board[2];

    private PuyoSpawner _spawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("ERROR : Multiple GameManager is Running");
            return;
        }
        Instance = this;

        _boards[0] = GameObject.Find("Player1").GetComponent<Board>();
        _boards[1] = GameObject.Find("Player2").GetComponent<Board>();

        _spawner = GetComponent<PuyoSpawner>();
    }

    /// <summary>
    /// �Ű������� ���� ���� -1�� ���� �ε����� ������ ���带 ��ȯ
    /// </summary>
    /// <param name="_playerNum"></param>
    /// �ڵ� �ۼ� �� ���Ǹ� ���� �ε������� 1 ū ���� �Ű������� ����
    /// ex) �÷��̾�1�� ���带 ��ȯ�ް� ���� ��� GameManager.Instance.GetBoard(1)
    ///     �÷��̾�2�� ���带 ��ȯ�ް� ���� ��� GameManager.Instance.GetBoard(2)
    ///     �� �Է��Ͽ� ���������� �� �� �ְ� ��
    /// <returns></returns>
    public Board GetBoard(int _playerNum) { return _boards[_playerNum - 1]; }

    public PuyoSpawner GetSpawner() { return _spawner; }
}
