using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    private float ITimer;
    public float ITime = 0.3f;
    private bool isDamaged;

    [Header("references")]
    public PlayerUI UI;

    [Header("Debug")]
    public bool KeyToDamage;
    public KeyCode DamageKey;
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

        if(Input.GetKey(DamageKey))
        {
            DamagePlayer(10f);
        }
    }
}
