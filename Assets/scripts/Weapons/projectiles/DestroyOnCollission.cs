using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class DestroyOnCollission : MonoBehaviour
{
    public float ExistTime = 5f;
    public float Speed;
    private float ExpireTimer;
    public float Damage;
    private float ParticleTime = 1f;
    private float ParticleTimer;
    public shotgunProjectile launcher;
    private bool UseExpireTimer;
    public ParticleSystem Sparks;
    public GameObject Blood;
    public bloodEmitter BloodParticles;
    private SphereCollider Collider;
    private bool CanCollide;

    private int LayersToIgnore = Convert.ToInt32("11111111111111111101111111111111", 2); //LAYER 1 IS AT THE END!!!!

    private void Start()
    {
        ExpireTimer = ExistTime;
        UseExpireTimer = true;
        CanCollide = true;
        Sparks = GetComponent<ParticleSystem>();
        BloodParticles = Blood.GetComponent<bloodEmitter>();
        Collider = GetComponent<SphereCollider>();
    }

    private void IfuckingHitShit(GameObject collision)
    {
        CanCollide = false;
        Collider.isTrigger = true;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject THEBLOOD = Instantiate(Blood);
            THEBLOOD.GetComponent<bloodEmitter>().AttachToObject(collision.transform.position);
            collision.transform.parent.gameObject.GetComponent<EnemyHealth>().health -= Damage;
            Destroy(this.gameObject);
        }
        else
        {

            ParticleTimer = ParticleTime;
            var emitParams = new ParticleSystem.EmitParams();
            UseExpireTimer = false;
            Sparks.Emit(emitParams, 5);
        }

    }

    bool Collision;
    private void FixedUpdate()
    {
        if(UseExpireTimer)
        {
            if (ExpireTimer > 0)
            {
                ExpireTimer -= Time.deltaTime;
            }
            else if (ExpireTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        } else
        {
            if (ParticleTimer > 0)
            {
                ParticleTimer -= Time.deltaTime;
            } else if (ParticleTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        RaycastHit Hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray,out Hit, 0.5f + Speed * Time.deltaTime, LayersToIgnore)){
           if(!Collision)
            {
                Collision = true;
                IfuckingHitShit(Hit.collider.gameObject);
            }
            
        } else
        {
           if(!Collision)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            }
        }
    }
}
