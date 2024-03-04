using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMessage : MonoBehaviour
{
    public TMP_Text TheText;
    public GameObject Panel;
    public AudioSource AudioSource;


    IEnumerator DisableMessageAfterTime(float Time)
    {
        yield return new WaitForSecondsRealtime(Time);
        Panel.SetActive(false);
    }
    public void SetMessage(string Text, float Duration)
    {
        AudioSource.Play();
        TheText.text = Text;
        Panel.SetActive(true);
        StartCoroutine(DisableMessageAfterTime(Duration));
    }
}
