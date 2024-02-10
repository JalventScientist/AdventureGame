using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RevolverFire : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    private bool CanFire;
    public float ReloadTime = 1.7f;
    private float ReloadTimer;
    public float maxDistance = 90f;
    public float Damage;
    public LayerMask WhatIsEnemy;
    public GameObject orientation;
    public LineRenderer RevolverBulletLine;
    public GameObject Blood;
    public bloodEmitter BloodParticles;
    public GameObject hitPos;
    private float length = 0f;

    private void Start()
    {
        orientation = GameObject.FindWithTag("orientation");
        BloodParticles = Blood.GetComponent<bloodEmitter>();
    }

    private void Update()
    {
        if (length > 0f)
        {
            length -= Time.deltaTime;
            RevolverBulletLine.startWidth = length;
        }
       
    }
    public void Fire()
    {
        Vector3 fwd = orientation.transform.TransformDirection(Vector3.forward);
        RaycastHit hitenemy;
        if(Physics.Raycast(orientation.transform.position, orientation.transform.forward, out hitenemy))
        {
            hitPos.transform.position = hitenemy.point;
            RevolverBulletLine.SetPosition(1, hitPos.transform.position);
            length = 0.7f;
            
            if(hitenemy.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                GameObject THEBLOOD = Instantiate(Blood);
                THEBLOOD.GetComponent<bloodEmitter>().AttachToObject(hitenemy.collider.transform.position);
                hitenemy.collider.gameObject.GetComponent<EnemyHealth>().health -= Damage;
            }
        }
    }
}
