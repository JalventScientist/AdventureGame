using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{

    [Header("Level Timer")]
    public bool TimerActive = false;
    public float TotalTime;

    private void Update()
    {
        if (TimerActive)
        {
            TotalTime += Time.deltaTime;
        }
    }

    public string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float Fraction = time * 1000;
        Fraction = (Fraction % 1000);
        string ResultText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, Fraction);
        return ResultText;
    }
}
