using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    public TMP_Text SpeedCounter;
    public Rigidbody PlayerObject;
    private void Update()
    {
        SpeedCounter.text = "Speed:" + Vector3.Magnitude(PlayerObject.velocity);
    }
}
