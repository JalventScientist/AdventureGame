using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class musicHandler : MonoBehaviour
{
    public bool CanGlitch;
    public bool GlitchHappening;
    public CauseGlitch GlitchEffect;

    [Header("Settings")]
    public float SwitchSpeedModifier;
    public float Volume = 1.0f;
    public float EnemyCount;
    public bool MusicStarted = false;
    public bool IsBoss;
    public bool FreezeState = false;
    [Header("References")]
    public GameObject NormalMusic;
    public GameObject ViolentMusic;
    public GameObject BossMusic;
    public GameObject PreAmbienceMusic;

    private AudioSource NormalSource;
    public AudioSource ViolentSource;
    private AudioSource BossSource;
    private AudioSource PreAmbienceSource;

    private AudioSource[] Sources = {null, null, null, null};

    private void Start()
    {
        
        ViolentSource = ViolentMusic.GetComponent<AudioSource>();
        NormalSource = NormalMusic.GetComponent<AudioSource>();
        PreAmbienceSource = PreAmbienceMusic.GetComponent<AudioSource>();
        BossSource = BossMusic.GetComponent<AudioSource>();

        

        if (!MusicStarted)
        {
            ViolentSource.volume = 0f;
            NormalSource.volume = 0f;
        } else
        {
            ViolentSource.volume = 0f;
            NormalSource.volume = Volume;
        }
        SpontaneousStart();
        Sources[0] = NormalSource;
       Sources[1] = ViolentSource;
       Sources[2] = BossSource;
       Sources[3] = PreAmbienceSource;


    }
    bool CanRestart = true;
    private void Update()
    {
        if(!FreezeState)
        {
            if (!GlitchHappening)
        {
            if (MusicStarted)
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
                    if (NormalSource.volume > 0.2f)
                    {
                        NormalSource.volume -= Time.deltaTime * SwitchSpeedModifier;
                    }
                    else
                    {
                        NormalSource.volume = 0f;
                    }
                }
                else // No Enemies left
                {
                    if (NormalSource.volume <= Volume)
                    {
                        NormalSource.volume += Time.deltaTime * SwitchSpeedModifier;

                    }
                    else
                    {
                        NormalSource.volume = Volume;
                    }
                    if (ViolentSource.volume > 0.2f)
                    {
                        ViolentSource.volume -= Time.deltaTime * SwitchSpeedModifier;
                    }
                    else
                    {
                        ViolentSource.volume = 0f;
                    }
                }
                if (PreAmbienceSource.volume > 0.2f)
                {
                    PreAmbienceSource.volume -= Time.deltaTime * SwitchSpeedModifier;
                }
                else
                {
                    PreAmbienceSource.volume = 0f;
                }
            }
            else
            {
                if (ViolentSource.volume > 0.2f)
                {
                    ViolentSource.volume -= Time.deltaTime * SwitchSpeedModifier;
                }
                else
                {
                    ViolentSource.volume = 0f;
                }
                if (NormalSource.volume > 0.2f)
                {
                    NormalSource.volume -= Time.deltaTime * SwitchSpeedModifier;
                }
                else
                {
                    NormalSource.volume = 0f;
                }
                if (PreAmbienceSource.volume <= Volume)
                {
                    PreAmbienceSource.volume += Time.deltaTime * SwitchSpeedModifier;

                }
                else
                {
                    PreAmbienceSource.volume = Volume;
                }
            }
        }
        if (NormalSource.volume < 0.02f)
            NormalSource.mute = true;
        else
            NormalSource.mute = false;
        if (ViolentSource.volume < 0.02f)
            ViolentSource.mute = true;
        else
            ViolentSource.mute = false;
        if (BossSource.volume < 0.02f)
            BossSource.mute = true;
        else
            BossSource.mute = false;
        if (PreAmbienceSource.volume < 0.02f)
            PreAmbienceSource.mute = true;
        else
            PreAmbienceSource.mute = false;
        }
    }

    public void SpontaneousStart()
    {
        ViolentSource.Play();
        NormalSource.Play();
        BossSource.Play();
        PreAmbienceSource.Play();
        GlitchHappening = false;
        CanGlitch = false;
        if (MusicStarted)
        {
            if (EnemyCount > 0)
            {
                ViolentSource.volume = Volume;
                NormalSource.volume = 0f;
                PreAmbienceSource.mute = true;
                ViolentSource.mute = false;
                NormalSource.mute = true;
            }
            else // No Enemies left
            {

                NormalSource.volume = Volume;
                ViolentSource.volume = 0f;
                PreAmbienceSource.volume = 0f;
                PreAmbienceSource.mute = true;
                ViolentSource.mute = true;
                NormalSource.mute = false;

            }
        }
        else
        {
            ViolentSource.volume = 0f;
            NormalSource.volume = 0f;
            PreAmbienceSource.volume = Volume;
            ViolentSource.mute = true;
            PreAmbienceSource.mute = false;
            NormalSource.mute = true;
        }
    }

    public void SetGlobalVolume(float volume, bool EndPermanently = false)
    {
        if (EndPermanently)
            CanRestart = false;
        else
            CanRestart = true;
        for (int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].mute)
            {
                Sources[i].DOFade(volume, 3f).SetUpdate(true);
            }
        }
    }
}