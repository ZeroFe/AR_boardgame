using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// ���� ���� �� ��ü ���� ���� �����ϴ� Ŭ����
/// </summary>
public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    // �ϴ� 2�� player, �� 2���� �ִٰ� ����
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

    // ���� ����
    // ù �÷��̾� ����

    public void TurnStart()
    {
        // �� ��ŸƮ ǥ��
        print("Turn Start!");
        debugText.text = "Turn Start";
    }

    public void MovePlayer(int dice)
    {
        print($"Current Player - {currentPlayer}, Dice : {dice}");

        // �� �̵�
        players[currentPlayer].MoveCount(dice);
    }

    // Ÿ�� ȿ�� ����

    // �� �÷��̾ ���� Ÿ���̸� ����
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

    // ���� ������ �ѱ��
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
