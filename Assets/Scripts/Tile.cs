using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 말이 밟고 있는 타일 정보를 저장하는 클래스
/// </summary>
public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        GoldenCard,
        Victory
    }

    public static float TILE_REVEAL_ANIM_TIME = 0.4f;

    public TileType tileType = TileType.Normal;
    public bool isMoved = false;
    //public TileEffect tileEffect;
    // 타일 속성

    private void Awake()
    {
        //tileEffect = GetComponent<TileEffect>();
        //if (tileEffect == null)
        //{
        //    tileEffect = gameObject.AddComponent<NoneEffect>();
        //}
    }

    public void ApplyTileEffect(Player target)
    {
        //tileEffect.ApplyTileEffect(target);
        // 팝업을 띄우고, 효과 적용
        switch (tileType)
        {
            case TileType.Normal:
                GameSystem.Instance.CheckBattle();
                break;
            case TileType.GoldenCard:
                // 골든 카드 매니저에서 랜덤하게 카드를 불러온다
                var card = GoldenCardManager.Instance.GetCard();
                PopupSystem.Instance.ApplyPopup(card.effectName, card.effectDescription, card.ApplyEffect, target);
                break;
            case TileType.Victory:
                GameSystem.Instance.Victory(target);
                break;
        }
    }
}
