using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 황금카드 목록을 불러오고 관리하는 클래스
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

    // 카드 셔플 
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
        // 이미 셔플된 카드 배열이므로 랜덤하게 뽑는 것과 같다 볼 수 있다
        // 따라서 index에 따라 순차적으로 뽑아온다
        var card = goldenCards[index];
        // 카드 개수가 적으면 OutOfIndex가 날 수 있으므로 방지
        index = (index + 1) % goldenCards.Length;

        return card;
    }
}
