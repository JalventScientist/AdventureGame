using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyOnCollission : MonoBehaviour
{
    public float ExistTime = 5f;
    private float ExpireTimer;
    public float Damage;
    private float ParticleTime = 1f;
    private float ParticleTimer;
    public shotgunProjectile launcher;
    private bool UseExpireTimer;
    public Rigidbody rb;
    public ParticleSystem Sparks;
    public GameObject Blood;
    public bloodEmitter BloodParticles;

    private void Start()
    {
        ExpireTimer = ExistTime;
        UseExpireTimer = true;
        rb = GetComponent<Rigidbody>();
        Sparks = GetComponent<ParticleSystem>();
        BloodParticles = Blood.GetComponent<bloodEmitter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       IfuckingHitShit(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        IfuckingHitShit(collision);
    }

    private void IfuckingHitShit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject THEBLOOD = Instantiate(Blood);
            THEBLOOD.GetComponent<bloodEmitter>().AttachToObject(collision.transform.position);
            collision.gameObject.GetComponent<EnemyHealth>().health -= Damage;
            Object.Destroy(this.gameObject);
        }
        else
        {

            rb.constraints = RigidbodyConstraints.FreezeAll;
            ParticleTimer = ParticleTime;
            var emitParams = new ParticleSystem.EmitParams();
            UseExpireTimer = false;
            Sparks.Emit(emitParams, 5);
        }

    }
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
                Object.Destroy(this.gameObject);
            }
        } else
        {
            if (ParticleTimer > 0)
            {
                ParticleTimer -= Time.deltaTime;
            } else if (ParticleTimer <= 0)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }
}
