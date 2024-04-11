using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLocker : MonoBehaviour
{
    public void OpenItch()
    {
        Application.OpenURL("https://jalvent.itch.io/terminus");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
