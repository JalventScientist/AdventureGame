using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text ComboCounter;

    public float Score; //Total score
    public float multiplier = 1f;

    public float ComboTime = 2f;
    private float ComboTimer;
    public float Decrement = 1f; // Affects the score drop speed; Best kept under 1f

    private void Update()
    {
        ComboCounter.text = "Multiplier: x" + multiplier;
        multiplier = Mathf.Round(multiplier * 10.0f) * 0.1f;
        multiplier = Mathf.Clamp(multiplier,1, 3);
        if(ComboTimer > 0)
        {
            ComboTimer -= Time.deltaTime;
        } else if (multiplier > 1)
        {
            multiplier -= (Time.deltaTime * Decrement);
        }
        if(multiplier > 1)
        {
            ComboCounter.gameObject.SetActive(true);
        } else
        {
            ComboCounter.gameObject.SetActive(false);
        }
    }

    public void NewKill() //Resets combo timer and adds 1 to combo
    {
        multiplier += 0.1f;
        ComboTimer = ComboTime;
    }
    public void AddScore(float AddedScore)
    {
        float FinalScore = AddedScore * multiplier; //Applies the score multiplier to the added score
        Score += FinalScore;
    }
}
