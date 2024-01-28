using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100; // Default value
    private GameObject MusicHandler;

    private void Start()
    {
        MusicHandler = GameObject.FindWithTag("musichandler");
    }

    private void Update()
    {
        if (health <= 0)
        {
            MusicHandler.GetComponent<musicHandler>().EnemyCount -= 1f;
            Object.Destroy(this.gameObject);
        }
    }
}
