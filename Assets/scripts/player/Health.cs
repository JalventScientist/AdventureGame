using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    private float ITimer;
    public float ITime = 0.1f;
    private bool isDamaged;
    public bool DeathConfirmed = false;

    [Header("references")]
    public PlayerUI UI;
    public DeathScreen DeathUI;
    public GameObject UIRenderer;
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void DamagePlayer(float DamageCount)
    {
       if(!isDamaged)
        {
            float RoundedDamage = Mathf.Round(DamageCount);
            CurrentHealth -= RoundedDamage;
            UI.changeHealthUI(true);
            isDamaged = true;
            ITimer = ITime;
            
        }
    }

    public void HealPlayer(float HealAmount)
    {
        float RoundedDamage = Mathf.Round(HealAmount);
        CurrentHealth += RoundedDamage;
        UI.changeHealthUI(true);
    }
    private void Update()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 100);
        if (isDamaged)
        {
             if(ITimer > 0)
            {
                ITimer -= Time.deltaTime;
            } else
            {
                isDamaged = false;
            }
        }
        if (CurrentHealth <= 0)
        {
            if(!DeathConfirmed)
            {
                DeathConfirmed = true;
                UIRenderer.SetActive(false);
                DeathUI.Die();
            }
        }
    }
}
