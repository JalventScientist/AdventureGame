using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class EndLevel : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    private LevelTimer Timer;
    private musicHandler musicHandler;
    public PauseGame PauseGame;

    [Header("References")]
    public RawImage UIFade;
    public TMP_Text ScoreText;
    public TMP_Text Title;
    public TMP_Text TimerText;
    public RawImage Effect;
    [Header("Buttons")]
    public GameObject NextLevelButton;
    public TMP_Text NextLevelText;
    public GameObject RestartLevelButton;
    public TMP_Text RestartLevelText;
    public GameObject MenuButton;
    public TMP_Text MenuText;

    public bool Triggered = false;

    private void Start()
    {
        Timer = GameObject.FindWithTag("LevelEnder").GetComponent<LevelTimer>();
        musicHandler = GameObject.FindWithTag("musichandler").GetComponent<musicHandler>();
        
    }


    IEnumerator RemoveText(TMP_Text TextToChange, float speedmodifier = 1f)
    {
        int CharacterCount = TextToChange.text.Length;
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.02f * speedmodifier);
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
        TextToChange.maxVisibleCharacters = 0;
        string Text = SetText;
        foreach (char c in Text)
        {
            TextToChange.maxVisibleCharacters += 1;

            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
    IEnumerator ChangeText(TMP_Text TextToChange, string TextToChangeTo)
    {
        StartCoroutine(RemoveText(TextToChange));
        yield return new WaitForSecondsRealtime(1f);
        TextToChange.maxVisibleCharacters = 0;
        StartCoroutine(SetText(TextToChange, TextToChangeTo));
    }

    IEnumerator EndLeLevel()
    {
        print("Called");
        PauseGame.CanPressMenu = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        musicHandler.FreezeState = true;
        Time.timeScale = 0f;
        musicHandler.SetGlobalVolume(0f);
        UIFade.DOColor(new Color(0, 0, 0, 1f), 1f).SetUpdate(true);
        Effect.DOColor(new Color(0.271f, 0.392f, 0.451f, 1f), 1f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.75f);
        StartCoroutine(SetText(Title, "LEVEL COMPLETED"));
        yield return new WaitForSecondsRealtime(0.5f);
        string LeStringa = "Time: " + Timer.FormatTime(Timer.TotalTime);
        string LaStringa = "Score: " + scoreSystem.Score;
        StartCoroutine(SetText(ScoreText, LaStringa));
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(SetText(TimerText, LeStringa));
        yield return new WaitForSecondsRealtime(0.5f);
        NextLevelButton.SetActive(true);
        RestartLevelButton.SetActive(true);
        MenuButton.SetActive(true);
        StartCoroutine(SetText(NextLevelText, "Next Level"));
        StartCoroutine(SetText(RestartLevelText, "Restart Level"));
        StartCoroutine(SetText(MenuText, "To Menu"));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!Triggered)
        {
            Timer.TimerActive = false;
            UIFade.transform.parent.gameObject.SetActive(true);
            Triggered = true;
            StartCoroutine(EndLeLevel());
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (!Triggered)
        {
            Timer.TimerActive = false;
            UIFade.transform.parent.gameObject.SetActive(true);
            Triggered = true;
            StartCoroutine(EndLeLevel());
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (!Triggered)
        {
            Timer.TimerActive = false;
            UIFade.transform.parent.gameObject.SetActive(true);
            Triggered = true;
            StartCoroutine(EndLeLevel());
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void NextLevel()
    {

    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void SetScoreSystem()
    {
        scoreSystem = GameObject.FindWithTag("Score").GetComponent<ScoreSystem>();
    }
}
