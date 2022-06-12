using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ȳ��ī�� ����� �ҷ����� �����ϴ� Ŭ����
/// </summary>
[DisallowMultipleComponent]
public class GoldenCardManager : MonoBehaviour
{
    public static GoldenCardManager Instance { get; private set; }

    private GoldenCard[] goldenCards;

    private int index = 0;

    private void Awake()
    {
        Instance = this;

        goldenCards = Resources.LoadAll<GoldenCard>("GoldenCard");
        Debug.Assert(goldenCards.Length > 0, "Error : There is no golden cards");

        Shuffle();
    }

    // ī�� ���� 
    public void Shuffle()
    {
        int n = goldenCards.Length;
        var random = new System.Random();
        while (n > 1)
        {
            int k = random.Next(n--);
            (goldenCards[n], goldenCards[k]) = (goldenCards[k], goldenCards[n]);
        }
    }

    public GoldenCard GetCard()
    {
        // �̹� ���õ� ī�� �迭�̹Ƿ� �����ϰ� �̴� �Ͱ� ���� �� �� �ִ�
        // ���� index�� ���� ���������� �̾ƿ´�
        var card = goldenCards[index];
        // ī�� ������ ������ OutOfIndex�� �� �� �����Ƿ� ����
        index = (index + 1) % goldenCards.Length;

        return card;
    }
}
