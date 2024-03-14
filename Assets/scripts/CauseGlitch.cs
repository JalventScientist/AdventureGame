using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using EZCameraShake;
using UnityEngine.UI;

public class CauseGlitch : MonoBehaviour
{
    public bool CanBeTriggered;
    private bool IsTriggered;

    public AudioClip ClairDeLune;
    [Header("Required Objects")]
    public GameObject GlitchText;
    public TMP_Text ActualText;
    public Image GlitchEffect;

    [Header("References")]
    public musicHandler moosic;
    public PlayerCam Shaker;
    public GameObject PlayerPosition;
    public GameObject GoToPos;

    private IEnumerator coroutine;
    IEnumerator ShakeText()
    {
        while (true)
        {
            Vector3 NewLocation = new Vector3(Screen.width * Random.Range(0.498f, 0.502f), Screen.height * Random.Range(0.495f, 0.505f), 0);
            Quaternion NewRotation = Quaternion.Euler(0, 0, Random.Range(-1, 1));
            GlitchText.GetComponent<RectTransform>().position = NewLocation;
            GlitchText.GetComponent<RectTransform>().rotation = NewRotation;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator StartGlitchinTfOut()
    {
        StartCoroutine(coroutine);
        ActualText.DOColor(new Color(1, 1, 1, 0.45490196078431372549019607843137f), 4f);
        GlitchEffect.DOColor(new Color(0.45490196078431372549019607843137f, 0.45490196078431372549019607843137f, 0.45490196078431372549019607843137f, 1), 4f);
        ActualText.text = "THE VEILS UNRAVEL";
        yield return new WaitForSeconds(2.5f);
        ActualText.text = "PERCEPTION SHATTERS";
        yield return new WaitForSeconds(2.5f);
        GlitchEffect.DOColor(new Color(0,0,0, 1), 1f);
        ActualText.text = "REALITY AWAKENS";
        PlayerPosition.transform.position = GoToPos.transform.position;
        yield return new WaitForSeconds(2.5f);
        StopCoroutine(coroutine);
        GlitchText.SetActive(false);
        GlitchEffect.gameObject.SetActive(false);

    }

    private void Start()
    {
        coroutine = ShakeText();
    }

    public void StartGlitching()
    {
        if (CanBeTriggered && !IsTriggered)
        {
            IsTriggered = true;
            StartCoroutine(StartGlitchinTfOut());
        }
    }
    //0.13725490196078431372549019607843
}
