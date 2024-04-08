using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100; // Default value
    private GameObject MusicHandler;
    private ScoreSystem ScoreSystem;

    private void Start()
    {
        MusicHandler = GameObject.FindWithTag("musichandler");
        ScoreSystem = GameObject.FindWithTag("Score").GetComponent<ScoreSystem>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            MusicHandler.GetComponent<musicHandler>().EnemyCount -= 1f;
            ScoreSystem.AddScore(Random.Range(250, 300));
            ScoreSystem.NewKill();
            Object.Destroy(this.gameObject);
        }
    }

    /* BUNDLE
    private ScoreSystem ScoreSystem;
    ScoreSystem = GameObject.FindWithTag("Score").GetComponent<ScoreSystem>();
    ScoreSystem.AddScore(Random.Range(250, 300)); // ADDS SCORE
    ScoreSystem.NewKill(); // ON KILL
     */
}
