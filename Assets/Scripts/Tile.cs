using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��� �ִ� Ÿ�� ������ �����ϴ� Ŭ����
/// </summary>
public class Tile : MonoBehaviour
{
    public TileEffect tileEffect;
    // Ÿ�� �Ӽ�

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
