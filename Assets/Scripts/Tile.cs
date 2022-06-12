using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��� �ִ� Ÿ�� ������ �����ϴ� Ŭ����
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
    // Ÿ�� �Ӽ�

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
        // �˾��� ����, ȿ�� ����
        switch (tileType)
        {
            case TileType.Normal:
                GameSystem.Instance.CheckBattle();
                break;
            case TileType.GoldenCard:
                // ��� ī�� �Ŵ������� �����ϰ� ī�带 �ҷ��´�
                var card = GoldenCardManager.Instance.GetCard();
                PopupSystem.Instance.ApplyPopup(card.effectName, card.effectDescription, card.ApplyEffect, target);
                break;
            case TileType.Victory:
                GameSystem.Instance.Victory(target);
                break;
        }
    }
}
