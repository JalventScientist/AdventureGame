using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSideChecker : MonoBehaviour
{
    public GameObject DoorToLetCheck;
    private Door DoorScript;
    public bool IsChunk1;

    //Was there an easier way to do this? Probably. Do i care? No.

    private void Start()
    {
        DoorScript = DoorToLetCheck.GetComponent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerCollider")
        {
            if (IsChunk1)
                DoorScript.Chunk1Triggered = true;
            else
                DoorScript.Chunk2Triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            if (IsChunk1)
                DoorScript.Chunk1Triggered = false;
            else
                DoorScript.Chunk2Triggered = false;
        }
    }
}
