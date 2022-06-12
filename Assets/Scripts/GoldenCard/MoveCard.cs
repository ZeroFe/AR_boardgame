using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move Card", menuName = "Golden Card/Move Card")]
public class MoveCard : GoldenCard
{
    [Tooltip("������ ������ Ƚ��\n(������ ������ �ڷ� �̵�)")]
    public int moveCount = 1;

    public override void ApplyEffect(Player target)
    {
        Debug.Log("Move Forward!");
        target.MoveCount(moveCount);
    }
}
