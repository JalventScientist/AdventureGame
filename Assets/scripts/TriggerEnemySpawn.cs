using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class TriggerEnemySpawn : MonoBehaviour
{
    public GameObject[] PrimaryWave;
    public bool WillTriggerGlitchOnceDead;

    public bool HasSecondWave;
    public GameObject[] SecondWave;

    public bool HasMessage = false;
    [TextArea(3,10)]
    public string MessageToSet;
    private PlayerMessage PlrMessage;
    public bool TriggersArena = false;
    public bool ArenaActive = false;

    public GameObject[] DoorsToLock;

    public int ActiveEntities;
    public Transform ArenaObject;

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
                if(TriggersArena)
                {
                    for(int i = 0; i < DoorsToLock.Length; i++)
                    {
                        DoorsToLock[i].GetComponent<Door>().TriggerArena(true);
                    }
                    StartCoroutine(SpawnWithDelay(0.5f));
                } else
                {
                    SpawnWave(1);
                }
                
            }
            Triggered = true;
        }
    }

    private void Update()
    {
        if (ArenaActive) {
            ActiveEntities = 0;
            foreach(Transform child in ArenaObject)
            {
                ActiveEntities++;
            }

            if(ActiveEntities <= 0) { //Arena cleared
                ArenaActive = false;
                for (int i = 0; i < DoorsToLock.Length; i++)
                {
                    DoorsToLock[i].GetComponent<Door>().TriggerArena(false);
                }
            }
        }
    }

    IEnumerator SpawnWithDelay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        SpawnWave(1);
        ArenaActive = true;
        if (WillTriggerGlitchOnceDead)
        {
            GameObject.FindWithTag("musichandler").GetComponent<musicHandler>().CanGlitch = true;
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