using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelDoor : MonoBehaviour
{
    public Animator DoorAnimator;
    public Light DoorLight;
    public BoxCollider DoorTrigger;
    public AudioSource DoorSound;
    public AudioClip[] DoorClips;

    public musicHandler musicHandler;

    public GameObject LevelChunk;

    private bool Triggered = false;

    public bool TriggeredByCollission = true;
    public void OpenDoor()
    {
        Triggered = true;
        DoorAnimator.Play("Open Door");
        DoorLight.DOIntensity(1f, 1f);
        StartCoroutine(DisableLight());
        if (TriggeredByCollission)
        {
            LevelChunk.SetActive(false);
        }
    }

    IEnumerator DisableLight()
    {
        yield return new WaitForSeconds(2f);
        DoorSound.clip = DoorClips[0];
        DoorSound.Play();
        musicHandler.MusicStarted = true;
        musicHandler.SpontaneousStart();
        if(TriggeredByCollission)
        {
            LevelChunk.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        DoorLight.DOIntensity(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DoorSound.Stop();
        DoorSound.clip = DoorClips[1];
        DoorSound.Play();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (TriggeredByCollission)
        {
            if (!Triggered)
            {
                if (collision.gameObject.tag == "PlayerCollider")
                {
                    OpenDoor();
                }
            }
        }
    }
}
