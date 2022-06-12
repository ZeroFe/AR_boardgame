using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 턴제 진행 및 전체 게임 룰을 관리하는 클래스
/// </summary>
public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    // 일단 2인 player, 말 2개만 있다고 가정
    public Player[] players;
    public int currentPlayer = 0;

    public GameObject battleField;

    [Header("UI")] 
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI playerTurnText;
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    public float GameScale => transform.localScale.y;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        battleField.SetActive(false);

        debugText.text = "Turn Start";
        // 가상의 
        currentPlayer = 1;
        NextTurn();
    }

    // 다음 턴으로 넘기기
    public void NextTurn()
    {
        currentPlayer = (currentPlayer + 1) % players.Length;
        print("current player : " + currentPlayer);
        playerTurnText.text = $"Player {currentPlayer} Turn";
        if (players[currentPlayer].rest >= 1)
        {
            // 플레이어 회복하는 애니메이션
            players[currentPlayer].rest--;
            NextTurn();
        }
        else
        {
            // 주사위를 던질 수 있게 만듦
        }
    }

    public void MovePlayer(int dice)
    {
        print($"Current Player - {currentPlayer}, Dice : {dice}");

        // 주사위 던졌으면 주사위 못 던지게 막기

        // 말 이동
        players[currentPlayer].MoveCount(dice);
    }

    // 두 플레이어가 같은 타일이면 전투
    public void CheckBattle()
    {
        print("Check Battle");
        if (players[0].pos != -1 && players[0].pos == players[1].pos)
        {
            // 임시 함수 : 이 위치에 전투를 실행하는 함수를 넣는다
            StartCoroutine(IEBattle());
        }
        else
        {
            print("No Battle");
            NextTurn();
        }
    }

    IEnumerator IEBattle()
    {
        print("Battle!");
        battleField.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        battleField.SetActive(false);

        BattleEnd();
    }

    public void BattleEnd()
    {
        // 플레이어들 위치 재선정
        players[0].transform.position = players[0].inTilePos.position;
        players[1].transform.position = players[1].inTilePos.position;

        // 플레이어들 체력 계산
        int player0Hp = players[0].hp;
        int player1Hp = players[1].hp;

        players[0].TakeDamage(player1Hp);
        players[1].TakeDamage(player0Hp);

        // 다음 턴으로 넘기기
        NextTurn();
    }
    // 

    public void Victory(Player player)
    {
        print("Victory!");
        victoryPanel.SetActive(true);
        victoryText.text = player == players[0] ? "플레이어 1 승리!" : "플레이어 2 승리!";
    }

    public void Restart()
    {
        //SceneManager.LoadScene()
    }
}
