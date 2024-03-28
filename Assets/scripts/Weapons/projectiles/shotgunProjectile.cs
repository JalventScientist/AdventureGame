using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    private bool CanFire;
    public float ReloadTime = 1f;
    private float ReloadTimer;
    public float MinSpread = -10f;
    public float MaxSpread = 10f;
    public float Damage;
    public void Fire()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(Random.Range(MinSpread, MaxSpread), Random.Range(MinSpread, MaxSpread), Random.Range(MinSpread, MaxSpread)));
        }
    }
}
