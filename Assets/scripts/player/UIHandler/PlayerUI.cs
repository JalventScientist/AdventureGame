using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerUI : MonoBehaviour
{
    [Header("UIs")]
    public GameObject Health;
    public GameObject Stamina;


    private RectTransform HealthUI;
    private RectTransform StaminaUI;
    public Graphic HealthColor;

    [Header("References")]
    public PlayerMovement plr;
    public Health plrHealth;

    private void Start()
    {
        StaminaUI = Stamina.GetComponent<RectTransform>();
        HealthUI = Health.GetComponent<RectTransform>();
    }

    private void Update()
    {
        StaminaUI.sizeDelta = new Vector2(-0.75f + ((plr.staminaLeft / plr.stamina) * 0.75f), 0f);
    }

    public void changeHealthUI(bool IsDamage)
    {
        if (IsDamage)
        { //FF7D64
            HealthColor.color = new Color(0.6823529411764706f, 0f,0f,1f);
            DOTweenModuleUI.DOColor(HealthColor, new Color(1f, 0.4901960784313725f, 0.392156862745098f, 1f),0.2f);
        }
        DOTweenModuleUI.DOSizeDelta(HealthUI, new Vector2(-0.75f + ((plrHealth.CurrentHealth / plrHealth.MaxHealth) * 0.75f), 0f),0.2f);
    }
}
