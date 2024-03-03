using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("References")]
    public TMP_Text TitleInfo;

    public GameObject MainMenuFrame;
    public GameObject LevelSelectFrame;
    public GameObject StageChoiceFrame;
    public GameObject LevelChoiceFrame;

    public TMP_Text[] MenuText;
    public string[] MenuTextStrings;

    public TMP_Text[] LevelText;
    public string[] LevelTextStrings;

    public TMP_Text[] Stages;
    public string[] StagesStrings;

    public TMP_Text[] LevelSelections;
    public string[] Levels0Strings;
    public string[] Levels1Strings;

    public string[] Levels0Strings_Long;
    public string[] Levels1Strings_Long;

    private bool StagesOpened = false;
    private bool SelectingLevel = false;
    bool AwaitingConfirmation;
    private int SelectedStage = 0;
    private int SelectedLevel = 0;

    private DataManager DataManager;

    public GameObject ConfirmLoadFrame;
    public TMP_Text AboutToPlay;
    public TMP_Text AboutToLevel;
    public TMP_Text AboutToButton;

    int CurrentStage;
    int CurrentLevel;

    private void Start()
    {
        DataManager = GameObject.FindWithTag("DataManager").GetComponent<DataManager>();
        TitleInfo.maxVisibleCharacters = TitleInfo.text.Length;
        for (int i = 0; i < MenuText.Length; i++)
        {
            MenuText[i].maxVisibleCharacters = MenuText[i].text.Length;
        }
        for (int i = 0; i < LevelText.Length; i++)
        {
            LevelText[i].maxVisibleCharacters = LevelText[i].text.Length;
        }

        CurrentStage = DataManager.CurrentStage;
        CurrentLevel = DataManager.CurrentLevel;
    }


    IEnumerator RemoveText(TMP_Text TextToChange, float speedmodifier = 1f)
    {
        int CharacterCount = TextToChange.text.Length;
        WaitForSeconds wait = new WaitForSeconds(0.02f * speedmodifier);
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
                if (LevelTextStrings[i] == "SELECT LEVEL")
                {
                    StartCoroutine(RemoveText(LevelText[i], 1.5f));
                } else
                {
                    StartCoroutine(RemoveText(LevelText[i]));
                }
                
            }
            yield return new WaitForSeconds(0.1f);
            MainMenuFrame.SetActive(true);
            LevelSelectFrame.SetActive(false);
            for (int i = 0; i < MenuText.Length; i++)
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

    IEnumerator OpenStages(bool Opened)
    {
        if(Opened)
        {
            StageChoiceFrame.SetActive(true);
            for (int i = 0; i < Stages.Length; i++)
            {
                StartCoroutine(SetText(Stages[i], StagesStrings[i]));
            }
            StagesOpened = true;
            yield return new WaitForEndOfFrame();
        } else
        {
            for (int i = 0; i < Stages.Length; i++)
            {
                StartCoroutine(RemoveText(Stages[i]));
            }
            StagesOpened = false;
            yield return new WaitForSeconds(0.1f);
            StageChoiceFrame.SetActive(false);
        }
        
    }

    IEnumerator ShowLevels(int Stage)
    {
        if (Stage != -1)
        {
            
            LevelChoiceFrame.SetActive(true);
            if (Stage == 0)
            {
                SelectedStage = 0;
                for (int i = 0; i < LevelSelections.Length; i++)
                {
                    if (DataManager.Stage0[i] == true)
                        LevelSelections[i].transform.parent.GetComponent<Button>().interactable = true;
                    else
                        LevelSelections[i].transform.parent.GetComponent<Button>().interactable = false;
                    StartCoroutine(SetText(LevelSelections[i], Levels0Strings[i]));
                }
            }
            else if (Stage == 1)
            {
                SelectedStage = 1;
                for (int i = 0; i < LevelSelections.Length; i++)
                {
                    if (DataManager.Stage1[i] == true)
                        LevelSelections[i].transform.parent.GetComponent<Button>().interactable = true;
                    else
                        LevelSelections[i].transform.parent.GetComponent<Button>().interactable = false;
                    StartCoroutine(SetText(LevelSelections[i], Levels1Strings[i]));
                }
            }
            yield return new WaitForEndOfFrame();
        } else
        {
            for (int i = 0; i < LevelSelections.Length; i++)
            {
                LevelSelections[i].transform.parent.GetComponent<Button>().interactable = false;
                StartCoroutine(RemoveText(LevelSelections[i]));
            }
            yield return new WaitForSeconds(0.1f);
            LevelChoiceFrame.SetActive(false);
            SelectingLevel = false;
        }
        
    }
    public void PlayButton(bool toggle)
    {
        if(!StagesOpened && !SelectingLevel)
        {
            if (toggle)
            {
                StartCoroutine(setMenu(false));
            }
            else
            {
                StartCoroutine(setMenu(true));
            }
        }
    }

    public void StartSelectingStage(bool toggle)
    {
        if(!SelectingLevel)
        {
            if (toggle)
            {
                if (!StagesOpened)
                {
                    StartCoroutine(OpenStages(true));
                }
            }
            else
            {
                if (StagesOpened)
                {
                    StartCoroutine(OpenStages(false));
                }
            }
        }
    }
    public void StartSelectingLevel(int StageNumber)
    {
        if (StagesOpened)
        {
            StartCoroutine(ShowLevels(StageNumber));
            SelectingLevel = true;
        } else if (StageNumber == -1) {
            StartCoroutine(ShowLevels(StageNumber));
        }
    }

    IEnumerator GoToLoadingText(int stage, int Level, bool WipeAllText = false)
    {
        if (WipeAllText)
        {
            for (int i = 0; i < LevelSelections.Length; i++)
            {
                LevelSelections[i].transform.parent.GetComponent<Button>().interactable = false;
                StartCoroutine(RemoveText(LevelSelections[i]));
            }
            for (int i = 0; i < Stages.Length; i++)
            {
                StartCoroutine(RemoveText(Stages[i]));
            }
            StagesOpened = false;
        }
        StartCoroutine(RemoveText(TitleInfo));
        for (int i = 0; i < LevelText.Length; i++)
        {
            if (LevelTextStrings[i] == "SELECT LEVEL")
            {
                StartCoroutine(RemoveText(LevelText[i], 1.5f));
            }
            else
            {
                StartCoroutine(RemoveText(LevelText[i]));
            }

        }
        yield return new WaitForSeconds(0.1f);
        LevelSelectFrame.SetActive(false);
        ConfirmLoadFrame.SetActive(true);
        StartCoroutine(SetText(AboutToPlay, "Loading The Following Level:"));
        if (stage == 0)
            StartCoroutine(SetText(AboutToLevel, Levels0Strings_Long[Level]));
        else if (stage == 1)
            StartCoroutine(SetText(AboutToLevel, Levels1Strings_Long[Level]));
        StartCoroutine(SetText(AboutToButton, "CONFIRM"));
        yield return new WaitForSeconds(0.1f);
        AwaitingConfirmation = true;
        SelectedLevel = Level;
        SelectedStage = stage; //Confirmation in case of ContinueLastLevel not having a prepared Stage
    }

    public void ContinueLastLevel()
    {
        if(!StagesOpened && !SelectingLevel)
        {
            StartCoroutine(GoToLoadingText(CurrentStage, CurrentLevel));
        }
    }

    public void SelectThisLevel(int Level)
    {
        SelectedLevel = Level;
        StartCoroutine(GoToLoadingText(SelectedStage, SelectedLevel, true));
    }

    public void ActuallyLoadTheLevel()
    {
        if (SelectedStage == 0)
        {
            if(SelectedLevel == 0)
            {
                DataManager.CurrentStage = 0;
                DataManager.CurrentLevel = 0;
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    IEnumerator DelayLevel()
    {
        yield return new WaitForSeconds(1);
        ActuallyLoadTheLevel();
    }
    public void StartLevel()
    {
        AboutToButton.transform.parent.GetComponent<Button>().interactable = false;
        AboutToButton.text = "LOADING";
        StartCoroutine(DelayLevel());
    }

    public void QuitGame()
    {
            Application.Quit();
    }

    private void Called()
    {
        print("Called");
    }
}
