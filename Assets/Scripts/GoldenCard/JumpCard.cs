using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Card", menuName = "Golden Card/Jump Card")]
public class JumpCard : GoldenCard
{
    [Tooltip("대상 타일의 Tile Manager에서의 위치")]
    public int tilePos = 0;

    public override void ApplyEffect(Player target)
    {
        Debug.Log("Jump!");
        target.MoveToTile(tilePos);
    }
}
