using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackward : TileEffect
{
    public int backwardCount = 1;

    public override void ApplyTileEffect(Player player)
    {
        print("Move Backward!");
        player.MoveCount(-backwardCount);
    }
}
