using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 말이 밟고 있는 타일 정보를 저장하는 클래스
/// </summary>
public class Tile : MonoBehaviour
{
    public TileEffect tileEffect;
    // 타일 속성

    private void Awake()
    {
        tileEffect = GetComponent<TileEffect>();
        if (tileEffect == null)
        {
            tileEffect = gameObject.AddComponent<NoneEffect>();
        }
    }

    public void ApplyTileEffect(Player player)
    {
        tileEffect.ApplyTileEffect(player);
    }
}
