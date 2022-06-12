using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ÿ���� ���� ������ ȿ��
/// </summary>
public abstract class GoldenCard : ScriptableObject
{
    [Tooltip("")]
    public string effectName;
    [TextArea(3, 5), Tooltip("ī�� ȿ���� ���� ����")]
    public string effectDescription;

    /// <summary>
    /// ������ ȿ�� ����
    /// ex) Ư�� Ÿ�Ϸ� ����, �� ĭ �̵� ��
    /// </summary>
    public abstract void ApplyEffect(Player target);
}
