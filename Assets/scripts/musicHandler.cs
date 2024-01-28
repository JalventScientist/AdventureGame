using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicHandler : MonoBehaviour
{
    [Header("Settings")]
    public float SwitchSpeedModifier;
    public float Volume = 1.0f;
    public float EnemyCount;
    public bool IsBoss;
    [Header("References")]
    public GameObject NormalMusic;
    public GameObject ViolentMusic;
    public AudioClip BossAudio;

    private AudioSource NormalSource;
    private AudioSource ViolentSource;

    private void Start()
    {
        if (IsBoss)
        {
            ViolentSource = ViolentMusic.GetComponent<AudioSource>();
            NormalSource = NormalMusic.GetComponent<AudioSource>();
            ViolentSource.volume = Volume;
            NormalSource.volume = 0f;
        } else
        {
            ViolentSource = ViolentMusic.GetComponent<AudioSource>();
            NormalSource = NormalMusic.GetComponent<AudioSource>();
            ViolentSource.volume = 0f;
            NormalSource.volume = Volume;
        }
    }

    private void Update()
    {
        if (EnemyCount > 0)
        {
            if (ViolentSource.volume <= Volume)
            {
                ViolentSource.volume += Time.deltaTime * SwitchSpeedModifier;

            }
            else
            {
                ViolentSource.volume = Volume;
            }
            if (NormalSource.volume > 0f)
            {
                NormalSource.volume -= Time.deltaTime * SwitchSpeedModifier;
            }
            else
            {
                NormalSource.volume = 0f;
            }
        } else // No Enemies left
        {
            if(NormalSource.volume <= Volume)
            {
                NormalSource.volume += Time.deltaTime * SwitchSpeedModifier;
                
            }
            else
            {
                NormalSource.volume = Volume;
            }
            if (ViolentSource.volume > 0f)
            {
                ViolentSource.volume -= Time.deltaTime * SwitchSpeedModifier;
            } else
            {
                ViolentSource.volume = 0f;
            }
        }
    }
}
