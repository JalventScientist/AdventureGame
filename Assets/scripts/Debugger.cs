using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    public TMP_Text SpeedCounter;
    public Rigidbody PlayerObject;
    public KeyCode PauseKey;
    public bool IsPaused;
    private void Update()
    {
        SpeedCounter.text = "Speed:" + Vector3.Magnitude(PlayerObject.velocity);
        if (Input.GetKey(PauseKey))
        {
            if (!IsPaused)
            {
                Time.timeScale = 0f;
                IsPaused = true;
                Debug.Log("Paused");
            }
            else
            {
                Time.timeScale = 1f;
                IsPaused = false;
            }

        }
    }
}
