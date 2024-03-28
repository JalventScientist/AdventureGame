using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PostKarenGlitch : MonoBehaviour
{
    [Header("Chunk Dependancies")]
    public GameObject PrimaryKarenChunk; // The store
    public GameObject GoryKarenChunk; // Blood
    public GameObject TheSkeleton; // Area with the skeleton
    public GameObject LandscapeOfThorns;
    public GameObject NewChunk; // Where the player Ends

    [Header("Other Dependencies")]
    public GameObject Player;
    public GameObject GlitchUI; // This has not been disabled after the first glitch so its still somewhat usable
    public Image GlitchEffect;
    public musicHandler Music;
    public AudioSource GlitchSound;
    public AudioClip Noise;
    private PlayerMovement PlayerMovement;

    [Header("Other Variables")]
    public bool CanBeTriggered = false;
    public bool HasDoneFirstWave = false;
    public bool HasDoneSecondWave = false;

    private float DebounceTimer;
    private bool Activated = false;

    //0:57 :>

    IEnumerator StartSecondGlitch()
    {
        GlitchEffect.gameObject.SetActive(true);
        CameraShaker.Instance.StartShake(2, 100, 1);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().CanMove = false;
        yield return new WaitForSeconds(Random.Range(1.001f, 1.701f));
        GlitchEffect.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        GlitchEffect.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(Random.Range(.751f, 1.001f));
        GlitchEffect.color = new Color(1, 1, 1, 1);
        GoryKarenChunk.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GlitchEffect.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(Random.Range(.251f, .751f));
    }

    private void Update()
    {
        if (CanBeTriggered)
        {
            if(DebounceTimer > 0) { 
                DebounceTimer -= Time.deltaTime;
            } else
            {
                if (Music.EnemyCount <= 0)
                {
                    if (!HasDoneFirstWave)
                    {
                        HasDoneFirstWave = true;
                        DebounceTimer = 0.5f;
                        print("FirstWave Done");
                    } else if(HasDoneFirstWave && !HasDoneSecondWave) {
                        HasDoneSecondWave = true;
                        DebounceTimer = 0.5f;
                    } else
                    {
                        if (!Activated)
                        {
                            Activated = true;
                            StartCoroutine(StartSecondGlitch());
                        }
                    }
                }
            }
        }

    }
}
