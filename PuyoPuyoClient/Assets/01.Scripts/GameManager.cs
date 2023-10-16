using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    //플레이어1, 2의 보드
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
    /// 매개변수로 들어온 값에 -1한 값을 인덱스로 가지는 보드를 반환
    /// </summary>
    /// <param name="_playerNum"></param>
    /// 코드 작성 시 편의를 위해 인덱스보다 1 큰 수를 매개변수로 받음
    /// ex) 플레이어1의 보드를 반환받고 싶을 경우 GameManager.Instance.GetBoard(1)
    ///     플레이어2의 보드를 반환받고 싶을 경우 GameManager.Instance.GetBoard(2)
    ///     로 입력하여 직관적으로 알 수 있게 함
    /// <returns></returns>
    public Board GetBoard(int _playerNum) { return _boards[_playerNum - 1]; }

    public PuyoSpawner GetSpawner() { return _spawner; }
}
