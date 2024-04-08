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
    bool ComboActive = false;
    bool ComboRestarted = false;

    private void Update()
    {
        ComboCounter.text = "Multiplier: x" + multiplier.ToString("0.00");
        multiplier = Mathf.Clamp(multiplier,1, 3);
    }

    public void NewKill() //Resets combo timer and adds 1 to combo
    {
        multiplier += 0.1f;

    }
}
