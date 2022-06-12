using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타일을 통해 적용할 효과
/// </summary>
public abstract class GoldenCard : ScriptableObject
{
    [Tooltip("")]
    public string effectName;
    [TextArea(3, 5), Tooltip("카드 효과에 대한 설명")]
    public string effectDescription;

    /// <summary>
    /// 말에게 효과 적용
    /// ex) 특정 타일로 점프, 몇 칸 이동 등
    /// </summary>
    public abstract void ApplyEffect(Player target);
}
