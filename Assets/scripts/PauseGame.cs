using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [Header("KeyCodes")]
    public KeyCode PauseKey = KeyCode.Escape;

    [Header("UI Components")]
    public Canvas MenuGUI;

    [Header("Check Data")]
    public bool CanPressMenu;

    [Header("Other Data")]
    public GameObject[] CheckPoints;
    public float lastCheckPoint = 0; // CheckPoint 0 = Start of level

    private bool HasPressedMenu = false;
    private bool MenuIsActive = false;
    public bool UIPressedContinue = false;

    private void Update()
    {
        if (CanPressMenu)
        {

            if (!HasPressedMenu)
            {
                if (Input.GetKeyDown(PauseKey))
                {
                    HasPressedMenu = true;
                    if(MenuIsActive)
                    {
                        TriggerPause(false);
                    } else
                    {
                        TriggerPause(true);
                    }
                }
            }
            if (Input.GetKeyUp(PauseKey))
                HasPressedMenu = false;
        }
    }
    public void TriggerPause(bool toggle)
    {
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            MenuGUI.gameObject.SetActive(true);
            MenuIsActive = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            MenuGUI.gameObject.SetActive(false);
            MenuIsActive = false;
        }
    }

    public void QuitLevel()
    {
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        } else
        {
            Application.Quit();
        }
    }

    public void RestartLevel(bool FromLastCheckPoint)
    {
        if(FromLastCheckPoint)
        {
            if (lastCheckPoint == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
