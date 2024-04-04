using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;

public class PostKarenGlitch : MonoBehaviour
{
    [Header("Chunk Dependancies")]
    public GameObject PrimaryKarenChunk; // The store
    public GameObject GoryKarenChunk; // Blood
    public GameObject TheSkeleton; // Area with the skeleton
    public GameObject LandscapeOfThorns;
    public GameObject NewChunk; // Where the player Ends

    public AudioClip Calm;
    public AudioClip Violent;

    public AudioSource PeacefulAudio;
    public AudioSource ViolentAudio;


    [Header("Other Dependencies")]
    public GameObject Player;
    public GameObject GlitchUI; // This has not been disabled after the first glitch so its still somewhat usable
    public Image GlitchEffect;
    public musicHandler Music;
    public AudioSource GlitchSound;
    public AudioClip Noise;
    public Image Blackout;
    private PlayerMovement PlayerMovement;
    public Transform GoToPos1;
    private AudioSource LeGlitchAudio;
    public Camera PlayerCamera; // Used to get the script that intensifies the camera effects based on where its looking
    public Transform TargetPoint;
    public AudioClip SnapBack;
    public AudioClip OtherSnap;
    public AudioSource SecondSnapAudio;
    public RawImage SnapBackToReality;
    public Transform ReturnPosition;

    private PlayerCam PlayerCam;

    private AudioSource HOLDONTOME;

    float MinValue = 0f;
    float MaxValue = 1f;

    bool DoDynamicIntensity = false;

    [Header("Post Process Adjustments")]
    public Volume Profile;
    private ChromaticAberration CA;
    private ColorAdjustments ColorAdjustments;
    private SplitToning ST;

    [Header("Other Variables")]
    public bool CanBeTriggered = false;
    public bool HasDoneFirstWave = false;
    public bool HasDoneSecondWave = false;

    private float DebounceTimer;
    private bool Activated = false;

    private float RequiredLookTime = 2f;
    private float LookTimer;

    private CameraShakeInstance lmao;

    private void Start()
    {
        Profile.profile.TryGet<ChromaticAberration>(out CA);
        Profile.profile.TryGet<ColorAdjustments>(out ColorAdjustments);
        Profile.profile.TryGet<SplitToning>(out ST);
        SnapBack.LoadAudioData();
        JUSTASINGLEPULL.LoadAudioData();
        OtherSnap.LoadAudioData();
        HOLDONTOME = GetComponent<AudioSource>();
        PlayerCam = PlayerCamera.GetComponent<PlayerCam>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }
    IEnumerator StartSecondGlitch()
    {
        SecondSnapAudio.gameObject.GetComponent<TMP_Text>().text = "";
        SecondSnapAudio.gameObject.SetActive(true);
        GlitchSound.clip = Noise;
        GlitchEffect.gameObject.SetActive(true);
        lmao = CameraShaker.Instance.StartShake(0.2f, 100, 3);
        lmao.DeleteOnInactive = false;
        PlayerMovement.CanMove = false;
        yield return new WaitForSeconds(Random.Range(1.001f, 1.701f));
        GlitchEffect.color = new Color(1, 1, 1, 1);
        GlitchSound.Play();
        yield return new WaitForSeconds(0.1f);
        GlitchSound.Pause();
        GlitchEffect.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(Random.Range(.751f, 1.001f));
        GlitchEffect.color = new Color(1, 1, 1, 1);
        GoryKarenChunk.SetActive(true);
        GlitchSound.Play();
        yield return new WaitForSeconds(0.1f);
        GlitchSound.Stop();
        GlitchSound.clip = OtherSnap;
        SecondSnapAudio.clip = SnapBack;
        GlitchEffect.color = new Color(1, 1, 1, 0);
        float wait = Random.Range(.251f, .751f);
        lmao.Magnitude = 2;
        DOTween.To(() => CA.intensity.value, x => CA.intensity.value = x, 1, wait);
        yield return new WaitForSeconds(wait);
        ColorAdjustments.active = true;
        yield return new WaitForSeconds(0.1f);
        GlitchSound.Play();
        PlayerCam.CameraEnabled = false;
        PlayerCam.SetOrientation(0, -90, 0);
        Blackout.color = new Color(0, 0, 0, 1f);
        yield return new WaitForSeconds(0.1f);
        GoryKarenChunk.SetActive(false);
        TheSkeleton.SetActive(true);
        Player.transform.position = GoToPos1.position;
        CA.intensity.value = 0f;
        yield return new WaitForSeconds(1f);
        SecondSnapAudio.Play();
        ColorAdjustments.active = false;
        PlayerCam.CameraEnabled = true;
        Blackout.color = new Color(0, 0, 0, 0f);
        DoDynamicIntensity = true;
        HOLDONTOME.Play();
        lmao.Magnitude = 1f;
    }

    public AudioClip JUSTASINGLEPULL;
    IEnumerator Glitchpart2()
    {
        var Chance = Random.Range(1, 10);
        HOLDONTOME.Stop();
        lmao.Magnitude = 5f;
        HOLDONTOME.clip = JUSTASINGLEPULL;
        GlitchSound.Play();
        PlayerCam.CameraEnabled = false;
        if (Chance == 1)
            SnapBackToReality.color = new Color(.1f, .1f, .1f, 1f);
        else
            Blackout.color = new Color(0, 0, 0, 1f);
        LandscapeOfThorns.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        HOLDONTOME.Play();
        SecondSnapAudio.Play();
        if (Chance == 1)
            SnapBackToReality.color = new Color(.1f, .1f, .1f, 0f);
        else
            Blackout.color = new Color(0, 0, 0, 0f);
        yield return new WaitForSeconds(0.5f);
        lmao.Magnitude = 0;
        HOLDONTOME.Stop();
        GlitchSound.Play();
        Blackout.color = new Color(0, 0, 0, 1f);
        LandscapeOfThorns.SetActive(false);
        NewChunk.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        CA.active = false;
        ColorAdjustments.active = false;
        ViolentAudio.clip = Violent;
        PeacefulAudio.clip = Calm;
        Player.transform.position = ReturnPosition.position;
        yield return new WaitForSeconds(0.2f);
        TheSkeleton.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        SecondSnapAudio.Play();
        PlayerMovement.CanMove = true;
        PlayerCam.CameraEnabled = true;
        Blackout.color = new Color(0, 0, 0, 0);
        Music.SpontaneousStart();
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
                        Music.CanGlitch = false;
                        HasDoneFirstWave = true;
                        DebounceTimer = 0.5f;
                    } else if(HasDoneFirstWave && !HasDoneSecondWave) {
                        HasDoneSecondWave = true;
                        DebounceTimer = 0.5f;
                    } else
                    {
                        
                        if (!Activated)
                        {
                            Music.SetGlobalVolume(0);
                            Activated = true;
                            StartCoroutine(StartSecondGlitch());
                        }
                    }
                }
            }
        }
        if(DoDynamicIntensity)
        {
            Vector3 TargetVector = PlayerCamera.transform.position -  TargetPoint.position;
            float angle = Vector3.Angle(PlayerCamera.transform.forward, TargetVector);
            float NormalizedAngle = (angle + 360f) % 360f;
            float MappedValue = Mathf.Lerp(MinValue, MaxValue, NormalizedAngle / 360f) * 2;
            HOLDONTOME.volume = MappedValue;
            //MappedValue = variable to change intensity over rotation
            CA.intensity.value = MappedValue;
            lmao.Magnitude = MappedValue * 2;
            if (MappedValue >= 0.93f)
            {
                if(LookTimer <= RequiredLookTime)
                {
                    DoDynamicIntensity = false;
                    StartCoroutine(Glitchpart2());
                } else
                {
                    LookTimer += Time.deltaTime;
                }
            }
        }
    }
}
