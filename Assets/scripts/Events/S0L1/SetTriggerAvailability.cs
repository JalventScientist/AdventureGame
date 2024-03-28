using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerAvailability : MonoBehaviour
{
    public PostKarenGlitch GlitchScript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerCollider")
        {
            GlitchScript.CanBeTriggered = true;
        }
    }
}
