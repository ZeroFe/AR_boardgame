using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JumpEffect : TileEffect
{
    public int jumpPos;

    public override void ApplyTileEffect(Player player)
    {
        print("Jump!");
        player.MoveToTile(jumpPos);
    }
}
