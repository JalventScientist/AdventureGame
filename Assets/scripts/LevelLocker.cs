using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLocker : MonoBehaviour
{
    public void OpenItch()
    {
        Application.OpenURL("https://discord.gg/GbCB9PkJKF");
    }
    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/j_scientist1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
