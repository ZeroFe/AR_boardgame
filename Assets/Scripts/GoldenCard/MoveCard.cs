using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move Card", menuName = "Golden Card/Move Card")]
public class MoveCard : GoldenCard
{
    [Tooltip("앞으로 움직일 횟수\n(음수를 넣으면 뒤로 이동)")]
    public int moveCount = 1;

    public override void ApplyEffect(Player target)
    {
        Debug.Log("Move Forward!");
        target.MoveCount(moveCount);
    }
}
