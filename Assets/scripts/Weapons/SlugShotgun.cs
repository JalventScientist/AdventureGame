using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugShotgun : MonoBehaviour
{
    [Header("GunStats")]
    private float ReloadTime = 0.958f;
    private float ReloadTimer;
    private bool CanFire;
    public float Damage;
    public Transform Transform;
    public Transform ShotGun;

    [Header("Requirements")]
    public Animator Animator;

    [Header("CheckData")]
    public bool hasEquipped = true;
    private void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
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
            Debug.Log("Waiting Until Fire");
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
        for(int i = 0; i < 10; i++)
        {

        }
        CameraShaker.Instance.ShakeOnce(4, 4, 0, 1);
        Animator.Play("Fire");
        ReloadTimer = ReloadTime;
        CanFire = false;
    }
}
