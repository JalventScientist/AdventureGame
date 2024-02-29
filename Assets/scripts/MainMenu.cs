using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [Header("References")]
    public TMP_Text TitleInfo;

        public GameObject MainMenuFrame;
        public GameObject LevelSelectFrame;
    public TMP_Text[] MenuText;
    public string[] MenuTextStrings;

    public TMP_Text[] LevelText;
    public string[] LevelTextStrings;

    private void Start()
    {
        TitleInfo.maxVisibleCharacters = TitleInfo.text.Length;
        for (int i = 0; i < MenuText.Length; i++)
        {
            MenuText[i].maxVisibleCharacters = MenuText[i].text.Length;
        }
        for (int i = 0; i < LevelText.Length; i++)
        {
            LevelText[i].maxVisibleCharacters = LevelText[i].text.Length;
        }
    }
    IEnumerator RemoveText(TMP_Text TextToChange)
    {
        int CharacterCount = TextToChange.text.Length;
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        while (CharacterCount > 0)
        {
            TextToChange.maxVisibleCharacters--;
            CharacterCount--;

            yield return wait;
        }
    }
    IEnumerator SetText(TMP_Text TextToChange, string SetText)
    {
        TextToChange.text = SetText;
        string Text = SetText;
        foreach (char c in Text)
        {
            TextToChange.maxVisibleCharacters += 1;

            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator ChangeText(TMP_Text TextToChange, string TextToChangeTo)
    {
        StartCoroutine(RemoveText(TextToChange));
        yield return new WaitForSeconds(1f);
        TextToChange.maxVisibleCharacters = 0;
        StartCoroutine(SetText(TextToChange, TextToChangeTo));
    }

    IEnumerator setMenu(bool MainOrLevel)
    {
        if(MainOrLevel)
        {
            StartCoroutine(ChangeText(TitleInfo, "Early Release (V0.1)"));
            for (int i = 0; i < LevelText.Length; i++)
            {
                StartCoroutine(RemoveText(LevelText[i]));
            }
            yield return new WaitForSeconds(0.1f);
            MainMenuFrame.SetActive(true);
            LevelSelectFrame.SetActive(false);
            for (int i = 0; i < LevelText.Length; i++)
            {
                StartCoroutine(ChangeText(MenuText[i], MenuTextStrings[i]));
            }
        } else
        {
            StartCoroutine(ChangeText(TitleInfo, "LEVEL SELECT"));
            for (int i = 0; i < MenuText.Length; i++) {
                StartCoroutine(RemoveText(MenuText[i]));
            }
            yield return new WaitForSeconds(0.1f);
            MainMenuFrame.SetActive(false);
            LevelSelectFrame.SetActive(true);
            for (int i = 0; i < LevelText.Length; i++)
            {
                StartCoroutine(ChangeText(LevelText[i], LevelTextStrings[i]));
            }
        }
    }
    public void PlayButton(bool toggle)
    {
        if(toggle)
        {
            StartCoroutine(setMenu(false));
        } else
        {
            StartCoroutine(setMenu(true));
        }
    }
}
