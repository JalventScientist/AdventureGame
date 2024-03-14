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
    private musicHandler moosic;
    public PlayerCam Shaker;
    public GameObject PlayerPosition;
    public GameObject GoToPos;
    public GameObject KarenChunk;
    public GameObject[] UnloadChunks;

    private IEnumerator coroutine;

    private float Intensity = 0f;
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
        yield return new WaitForSeconds(2f);
        StartCoroutine(coroutine);
        ActualText.DOColor(new Color(1, 1, 1, 0.1f), 3f);
        GlitchEffect.DOColor(new Color(0.45490196078431372549019607843137f, 0.45490196078431372549019607843137f, 0.45490196078431372549019607843137f, .5f), 5f);
        ActualText.text = "THE VEILS UNRAVEL";
        yield return new WaitForSeconds(2.5f);
        ActualText.text = "PERCEPTION SHATTERS";
        yield return new WaitForSeconds(2.5f);
        GlitchEffect.DOColor(new Color(0,0,0, 1), 1f);
        ActualText.text = "REALITY AWAKENS";
        for(int i = 0; i <UnloadChunks.Length; i++)
        {
            UnloadChunks[i].SetActive(false);
        }
        KarenChunk.SetActive(true);
        PlayerPosition.transform.position = GoToPos.transform.position;
        yield return new WaitForSeconds(2.5f);
        StopCoroutine(coroutine);
        GlitchText.SetActive(false);
        GlitchEffect.gameObject.SetActive(false);


    }

    private void Start()
    {
        coroutine = ShakeText();
        moosic = GameObject.FindWithTag("musichandler").GetComponent<musicHandler>();
    }

    public void StartGlitching()
    {
        if (CanBeTriggered && !IsTriggered)
        {
            IsTriggered = true;
            StartCoroutine(StartGlitchinTfOut());
        }
    }

    private void Update()
    {
        if (CanBeTriggered)
        {
            if(moosic.EnemyCount <= 0)
            {
                StartGlitching();
            }
        }
    }
    //0.13725490196078431372549019607843
}
