using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PostKarenGlitch : MonoBehaviour
{
    [Header("Chunk Dependancies")]
    public GameObject PrimaryKarenChunk;
    public GameObject GoryKarenChunk;
    public GameObject TheSkeleton;

    [Header("Other Dependencies")]
    public GameObject Player;
    public GameObject GlitchUI; // This has not been disabled after the first glitch so its still somewhat usable
    public musicHandler Music;

    [Header("Other Variables")]
    public bool CanBeTriggered = false;
    public bool HasDoneFirstWave = false;
    public bool HasDoneSecondWave = false;

    private float DebounceTimer;
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
                        print("Kill me PLEASE");
                    }
                }
            }
        }

    }
}
