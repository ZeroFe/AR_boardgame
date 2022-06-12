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
/// ���� ���� �� ��ü ���� ���� �����ϴ� Ŭ����
/// </summary>
public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    // �ϴ� 2�� player, �� 2���� �ִٰ� ����
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
        // ������ 
        currentPlayer = 1;
        NextTurn();
    }

    // ���� ������ �ѱ��
    public void NextTurn()
    {
        currentPlayer = (currentPlayer + 1) % players.Length;
        print("current player : " + currentPlayer);
        playerTurnText.text = $"Player {currentPlayer} Turn";
        if (players[currentPlayer].rest >= 1)
        {
            // �÷��̾� ȸ���ϴ� �ִϸ��̼�
            players[currentPlayer].rest--;
            NextTurn();
        }
        else
        {
            // �ֻ����� ���� �� �ְ� ����
        }
    }

    public void MovePlayer(int dice)
    {
        print($"Current Player - {currentPlayer}, Dice : {dice}");

        // �ֻ��� �������� �ֻ��� �� ������ ����

        // �� �̵�
        players[currentPlayer].MoveCount(dice);
    }

    // �� �÷��̾ ���� Ÿ���̸� ����
    public void CheckBattle()
    {
        print("Check Battle");
        if (players[0].pos != -1 && players[0].pos == players[1].pos)
        {
            // �ӽ� �Լ� : �� ��ġ�� ������ �����ϴ� �Լ��� �ִ´�
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
        // �÷��̾�� ��ġ �缱��
        players[0].transform.position = players[0].inTilePos.position;
        players[1].transform.position = players[1].inTilePos.position;

        // �÷��̾�� ü�� ���
        int player0Hp = players[0].hp;
        int player1Hp = players[1].hp;

        players[0].TakeDamage(player1Hp);
        players[1].TakeDamage(player0Hp);

        // ���� ������ �ѱ��
        NextTurn();
    }
    // 

    public void Victory(Player player)
    {
        print("Victory!");
        victoryPanel.SetActive(true);
        victoryText.text = player == players[0] ? "�÷��̾� 1 �¸�!" : "�÷��̾� 2 �¸�!";
    }

    public void Restart()
    {
        //SceneManager.LoadScene()
    }
}
