using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bloodEmitter : MonoBehaviour
{
    private float ExistLength = 3f;
    private float ExistTimer;
    private bool TimerEnabled = false;
    public GameObject Blood;
    public ParticleSystem BloodParticles;

    private void Start()
    {
        BloodParticles = GetComponent<ParticleSystem>();
    }

    public void AttachToObject(Vector3 Position)
    {
        Blood.transform.position = Position;
        ExistTimer = ExistLength;
        TimerEnabled = true;
    }
    private void FixedUpdate()
    {
        if(TimerEnabled)
        {
            if (ExistTimer > 0f)
            {
                ExistTimer -= Time.deltaTime;
            } else
            {
                Object.Destroy(Blood);
            }
        }
    }
}
