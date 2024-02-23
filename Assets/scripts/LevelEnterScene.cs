using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using DG.Tweening;

public class LevelEnterScene : MonoBehaviour
{
    //CameraShaker.Instance.ShakeOnce(4, 4, 0, 1);
    [Header("Object Requirements")]
    public GameObject Player;
    public Animator PlayerAnimator;
    public GameObject CameraDisable;
    public GameObject Crosshair;
    public GameObject Fade;
    private RawImage Image;
    [Header("Other Values")]
    private bool AnimationPlaying = false;
    public bool TriggerOrTimer;
    public float SpawnDelay;
    private float SpawnTimer;
    public float AnimationTime;
    private float AnimationTimer;
    private bool Spawning;
    private float ShakeTime = 0.375f;
    private float ShakeTimer;
    bool hasShaken = false;
    private float FadeTime = 4.7f;
    private float FadeTimer;
    bool hasFaded = false;
    void Start()
    {
        Image = Fade.GetComponent<RawImage>();
        if (TriggerOrTimer)
        {
            SpawnTimer = SpawnDelay;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (TriggerOrTimer)
        {
          if (!Spawning)
            {
                if (SpawnTimer > 0)
                    SpawnTimer -= Time.deltaTime;
                else
                    SpawnPlayer();
            }
        }
        if (AnimationPlaying)
        {
            if (AnimationTimer > 0)
            {
                AnimationTimer -= Time.deltaTime;
            }
            else if (AnimationTimer <= 0)
            {
                Player.SetActive(true);
                PlayerAnimator.gameObject.SetActive(false);
                CameraDisable.SetActive(false);
                Crosshair.SetActive(true);
                
            }
            if(!hasShaken)
            {
                if (ShakeTimer > 0)
                {
                    ShakeTimer -= Time.deltaTime;
                } else
                {
                    CameraShaker.Instance.ShakeOnce(2, 10, 0, 2);
                    hasShaken = true;
                }

            }
            if (FadeTimer > 0)
            {
                FadeTimer -= Time.deltaTime;
            }
            else
                if (!hasFaded)
            {
                hasFaded = true;
                FadeUI();
            }
            else
            {


            }
                        
        }
    }

    public void SpawnPlayer()
    {
        Spawning = true;
        PlayerAnimator.Play("Spawn");
        AnimationTimer = AnimationTime;
        AnimationPlaying = true;
        ShakeTimer = ShakeTime;
        FadeTimer = FadeTime;
    }

    private void FadeUI()
    {
        DOTweenModuleUI.DOColor(Image, new Color(0, 0, 0, 1), 0.5f).OnComplete(KILL);
    }
    private void KILL()
    {
        DOTweenModuleUI.DOColor(Image, new Color(0, 0, 0, 0), 0.2f);
    }
}
