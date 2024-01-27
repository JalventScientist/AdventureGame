using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100; // Default value

    private void Update()
    {
        if (health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
