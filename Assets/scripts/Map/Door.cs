using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool Chunk1Triggered;
    public bool Chunk2Triggered;

    public bool LastInteractedChunk = true; // True = Chunk 1, False = Chunk 2

    public GameObject Chunk1;
    public GameObject Chunk2;

    public Animator DoorAnimator;
    public Animator ArenaAnimator;

    public bool LockDoorsOnArena;

    public GameObject ArenaSpikes;
    public GameObject ArenaCollider;

    private float DebounceTimer;
    private float DebounceTime = 0.5f;
    public bool CanOpenDoor;
    private bool DoorOpen = false;

    private void Update()
    {
        if (CanOpenDoor)
        {
            if (Chunk1Triggered || Chunk2Triggered)
            {
                if (DebounceTimer == 0f)
                {
                    TriggerDoor(true);
                }

            } else
            {
                if (DebounceTimer == 0f)
                {
                    TriggerDoor(false);
                }
            }
        } else
        {
            if (DebounceTimer == 0f)
            {
                TriggerDoor(false);
            }
        }
        if(DebounceTimer > 0f)
            DebounceTimer -= Time.deltaTime;
        else
        {
            DebounceTimer = 0f;
        }
        if (Chunk1Triggered)
            LastInteractedChunk = true;
        else if (Chunk2Triggered)
            LastInteractedChunk = false;
    }

    IEnumerator SetActiveChunk(bool LoadChunk1, bool IsClosing)
    {
        if (!IsClosing)
        {
            Chunk1.SetActive(true);
            Chunk2.SetActive(true);
            DoorOpen = true;
        } else
        {
            yield return new WaitForSeconds(DebounceTime);
            if(LoadChunk1)
            {
                Chunk1.SetActive(true);
                Chunk2.SetActive(false);
            } else
            {
                Chunk1.SetActive(false);
                Chunk2.SetActive(true);
            }
        }
    }
    public void TriggerDoor(bool toggle)
    {
        if (toggle)
        {
            DoorAnimator.Play("Open");
            StartCoroutine(SetActiveChunk(LastInteractedChunk, false));
        } else
        {
            DoorAnimator.Play("Close");
            StartCoroutine(SetActiveChunk(LastInteractedChunk, true));
        }
        DebounceTimer = DebounceTime;
    }

    public void TriggerArena(bool toggle)
    {
        print("Called Arena with " + toggle);
        if (toggle)
        {
            ArenaAnimator.Play("Arena");
            ArenaCollider.SetActive(true);
            CanOpenDoor = false;
        } else
        {
            ArenaCollider.SetActive(false);
            ArenaAnimator.Play("UnArena");
            CanOpenDoor = true;
        }
    }
}
