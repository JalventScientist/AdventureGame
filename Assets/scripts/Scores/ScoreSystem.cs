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
        ComboCounter.text = "Multiplier: x" + Mathf.Round(multiplier * 100.0f) * 0.01f;
        Score = Mathf.Round(Score);
        ScoreText.text = "Score: " + Score;
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
            ComboCounter.color = new Color(1f, 0.984f, 0f, 1);
        } else
        {
            ComboCounter.color = new Color(1f, 0.984f, 0f, 0);
        }
    }

    public void NewKill() //Resets combo timer and adds 1 to combo
    {
        multiplier += 0.1f;
        ComboTimer = ComboTime;
    }
    public void AddScore(float AddedScore)
    {
        float FinalScore = (AddedScore * multiplier) * 0.6f; //Applies the score multiplier to the added score
        Score += FinalScore;
        ComboTimer = ComboTime;
    }
}
