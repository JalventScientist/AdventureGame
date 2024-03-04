using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] PrimaryWave;

    public bool HasSecondWave;
    public GameObject[] SecondWave;

    public bool HasMessage = false;
    [TextArea(3,10)]
    public string MessageToSet;
    private PlayerMessage PlrMessage;

    bool Triggered;

    private void Start()
    {
        PlrMessage = GameObject.FindWithTag("PlrMessageSystem").GetComponent<PlayerMessage>();
    }

    private void OnTriggerEnter(Collider Collission)
    {
        if (!Triggered)
        {
            if (Collission.gameObject.tag == "PlayerCollider")
            {
                SpawnWave(1);
            }
            Triggered = true;
        }
    }

    public void SpawnWave(int WaveNumer)
    {
        if(WaveNumer == 1)
        {
            for(int i = 0; i < PrimaryWave.Length; i++)
            {
                PrimaryWave[i].GetComponent<Spawner>().Spawn();
            }
        } else
        {
            if(HasSecondWave)
            {
                for (int i = 0; i < SecondWave.Length; i++)
                {
                    SecondWave[i].GetComponent<Spawner>().Spawn();
                }
            }
        }
        if (HasMessage)
        {
            PlrMessage.SetMessage(MessageToSet, 7f);
        }
    }
}