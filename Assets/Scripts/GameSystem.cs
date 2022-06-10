using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    private int currentPlayer = 0;

    [Header("UI")] 
    public TextMeshProUGUI debugText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TurnStart();
    }

    // 턴제 구현
    // 첫 플레이어 설정

    public void TurnStart()
    {
        // 턴 스타트 표시
        print("Turn Start!");
        debugText.text = "Turn Start";
    }

    public void MovePlayer(int dice)
    {
        print($"Current Player - {currentPlayer}, Dice : {dice}");

        // 말 이동
        players[currentPlayer].MoveCount(dice);
    }

    // 타일 효과 적용

    // 두 플레이어가 같은 타일이면 전투
    public void Battle()
    {
        print("In Battle");
        if (players[0].pos != -1 && players[0].pos == players[1].pos)
        {
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
        yield return new WaitForSeconds(0.5f);

        NextTurn();
    }

    // 다음 턴으로 넘기기
    public void NextTurn()
    {
        currentPlayer = (currentPlayer + 1) % players.Length;
        print("current player : " + currentPlayer);
    }
    // 

    public void Victory()
    {
        print("Victory!");

    }
}
