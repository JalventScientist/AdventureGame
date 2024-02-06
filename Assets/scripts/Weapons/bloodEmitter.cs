using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bloodEmitter : MonoBehaviour
{
    private float ExistLength = 20f;
    private float ExistTimer;
    private bool TimerEnabled = false;
    public GameObject Blood;
    public ParticleSystem BloodParticles;
    public AudioClip[] BloodSound;
    public GameObject BloodSounds;
    private AudioSource SoundSource;

    private void Start()
    {
        BloodParticles = GetComponent<ParticleSystem>();
        SoundSource = BloodSounds.GetComponent<AudioSource>();
    }

    public void AttachToObject(Vector3 Position)
    {
        Blood.transform.position = Position;
        ExistTimer = ExistLength;
        TimerEnabled = true;
        BloodSounds.GetComponent<AudioSource>().clip = BloodSound[Random.Range(0, 3)];
        BloodSounds.GetComponent<AudioSource>().Play();
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
