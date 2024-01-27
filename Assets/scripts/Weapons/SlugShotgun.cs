using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugShotgun : MonoBehaviour
{
    [Header("GunStats")]
    public float ReloadTime = 0.958f;
    private float ReloadTimer;
    public bool CanFire;
    public Transform Transform;
    public Transform ShotGun;
    public AudioSource Sound;
    public AudioClip[] ClipSources;
    private int AudioVariant; //loops between 3 numbers

    [Header("Requirements")]
    public Animator Animator;

    [Header("CheckData")]
    public bool hasEquipped = true;
    private void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        Sound = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (hasEquipped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(LoopFire());

            }
            else if (Input.GetMouseButtonUp(0))
            {
                 StopCoroutine(LoopFire());
            }

        }
    }

    IEnumerator LoopFire()
    {
        if (CanFire) {
            Fire();
        }
        else
        {
            yield return null;
        }
    }
    private void FixedUpdate()
    {

        ShotGun.localScale = new Vector3(50,40,50);
 
        if (!CanFire)
        {
            ReloadTimer -= Time.deltaTime;
            if(ReloadTimer <= 0)
            {
                Animator.Play("Idle");
                CanFire = true;
            }
        }
    }
    private void Fire()
    {
        Sound.clip = ClipSources[Random.Range(0, 2)];
        Sound.PlayOneShot(Sound.clip);
        
        CameraShaker.Instance.ShakeOnce(4, 4, 0, 1);
        Animator.Play("Fire");
        ReloadTimer = ReloadTime;
        CanFire = false;
    }
}
