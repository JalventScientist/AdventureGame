using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FilthBehaviour : MonoBehaviour
{
    [Header("Activation")]
    public bool BehaviourEnabled;

    [Header("References")]
    public Animator FilthAnimator;
    public GameObject FilthPrefab;
    public NavMeshAgent FilthAgent;
    private GameObject Player;
    private Vector3 movementDirection;
    private Vector3 lastMovement;
    private bool attacking = false;
    private bool grounded;
    private float latestDirectionChangeTime;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        FilthAnimator = FilthPrefab.GetComponent<Animator>();
    }

    private void Update()
    {
        if (BehaviourEnabled)
        {
            FilthAgent.SetDestination(Player.transform.position);
            
        }

        if(FilthAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if (lastMovement != transform.position)
            {
                FilthAnimator.Play("Run");
            }
            else
            {
                if (!attacking)
                {
                    FilthAnimator.Play("Idle");
                }
                else
                {
                    FilthAnimator.Play("Attack");
                }
            }
        } else
        {
            FilthAnimator.Play("Idle");
        }

        lastMovement = transform.position;
    }
}
