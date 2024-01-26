using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunProjectile : MonoBehaviour
{
    [Header("Data")]
    public float force; //Defines how fast the bullet should go
    private bool hitGround = false;

    [Header("References")]
    public Rigidbody projectileBody;

    private void Start()
    {
        projectileBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
}
