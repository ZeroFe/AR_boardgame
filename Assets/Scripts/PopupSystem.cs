using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupSystem : MonoBehaviour
{
    public static PopupSystem Instance { get; private set; }

    public delegate void TileEffectHandler(Player target);


    public TileEffectHandler tileEffect;

    public GameObject popup;
    [SerializeField] private TextMeshProUGUI effectName;
    [SerializeField] private TextMeshProUGUI effectDescription;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyPopup(string name, string description, TileEffectHandler effect, Player target)
    {
        effectName.text = name;
        effectDescription.text = description;
        tileEffect = effect;

        // 애니메이션
        StartCoroutine(IEAnim(effect, target));
    }

    IEnumerator IEAnim(TileEffectHandler effect, Player target)
    {
        popup.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        popup.SetActive(false);

        effect(target);
    }
}
