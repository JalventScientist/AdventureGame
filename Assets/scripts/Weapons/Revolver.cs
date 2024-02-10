using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    [Header("GunStats")]
    public float ReloadTime = 0.875f;
    private float ReloadTimer;
    public bool CanFire;
    private bool firing;
    public AudioSource Sound;
    public AudioClip[] ClipSources;
    public RevolverFire bullets;
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
                firing = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                firing = false;
            }

        }
    }
    private void FixedUpdate()
    {

        if (firing)
        {
            if (CanFire)

            {
                Fire();
            }
        }
        if (!CanFire)
        {
            ReloadTimer -= Time.deltaTime;
            if (ReloadTimer <= 0)
            {
                Animator.Play("Idle");
                CanFire = true;
            }
        }
    }
    private void Fire()
    {
        ReloadTimer = ReloadTime;
        CanFire = false;
        Sound.clip = ClipSources[Random.Range(0, 2)];
        Sound.PlayOneShot(Sound.clip);
        bullets.Fire();
        CameraShaker.Instance.ShakeOnce(4, 4, 0, 1);
        Animator.Play("Fire");

    }
}
