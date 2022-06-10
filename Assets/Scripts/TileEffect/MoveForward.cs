using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : TileEffect
{
    public int forwardCount = 1;

    public override void ApplyTileEffect(Player player)
    {
        print("Move Forward!");
        player.MoveCount(forwardCount);
    }
}
