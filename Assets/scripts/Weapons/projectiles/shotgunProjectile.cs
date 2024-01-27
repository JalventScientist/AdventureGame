using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class shotgunProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    private bool CanFire;
    private float ReloadTime = 0.958f;
    private float ReloadTimer;
    public float MinSpread = -10f;
    public float MaxSpread = 10f;
    public float Damage;

    private void Start()
    {
        CanFire = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (CanFire)
            {
                StartCoroutine(LoopFire());
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator LoopFire()
    {
        if (CanFire)
        {
            Fire();
        }
        else
        {
            yield return null;
        }
    }
    private void FixedUpdate()
    {

        if (!CanFire)
        {
            ReloadTimer -= Time.deltaTime;
            if (ReloadTimer <= 0)
            {
                CanFire = true;
            }
        }
    }

    private void Fire()
    {
        ReloadTimer = ReloadTime;
        CanFire = false;
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(Random.Range(MinSpread, MaxSpread), Random.Range(MinSpread, MaxSpread), Random.Range(MinSpread, MaxSpread)));
            bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        }
    }
}
