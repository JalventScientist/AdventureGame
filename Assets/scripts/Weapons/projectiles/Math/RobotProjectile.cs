using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectile : MonoBehaviour
{
    public QuadraticCurve curve;
    public float speed;
    private Health PlayerHealth;
    private GameObject Player;
    public float Damage = 10f;

    private float sampleTime;

    private void Start()
    {
        sampleTime = 0f;
        Player = GameObject.FindWithTag("Player");
        PlayerHealth = Player.GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            PlayerHealth.DamagePlayer(Damage);
        }

    }

    private void Update()
    {
        if(curve != null)
        {
            sampleTime += Time.deltaTime * speed;
            transform.position = curve.evaluate(sampleTime);
            transform.forward = curve.evaluate(sampleTime + 0.001f) - transform.position;

            if (sampleTime >= 1f)
            {
                Debug.Log("Death.mp3");
                Destroy(gameObject);
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
