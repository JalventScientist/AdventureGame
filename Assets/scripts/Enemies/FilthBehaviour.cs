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
    private Health PlayerHealth;
    private PlayerMovement PlayerMovement;
    private Vector3 movementDirection;
    private Vector3 lastMovement;
    private bool attacking = false;
    private bool grounded;
    private float latestDirectionChangeTime;
    public float AttackTime = (1f + ((1/3)*2));
    private float AttackTimer;
    private float MinimumDistance = 1.6f;
    public float ActualAttackTime = 0.75f;
    private float ActualAttackTimer;
    private bool HasAttacked = false;
    public float SpeedModifier = 1f;
    public float AttackDamage;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerHealth = Player.GetComponent<Health>();
        FilthAnimator = FilthPrefab.GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (BehaviourEnabled)
        {
            float Distance = Vector3.Distance(Player.transform.position, transform.position);

            FilthAgent.destination = Player.transform.position;
            if (Distance > MinimumDistance)
            {
                if (!attacking)
                {
                    if (FilthAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        if (lastMovement != transform.position)
                        {
                            FilthAnimator.Play("Run");
                        }
                    }
                    else
                    {
                        FilthAnimator.Play("Idle");
                    }
                }
                lastMovement = transform.position;
            }
            else
            {
                if (!attacking)
                {
                    Attack();
                }
            }
            if (AttackTimer > 0)
            {
                FilthAgent.isStopped = true;
                AttackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                FilthAgent.isStopped = false;
            }
            if (attacking)
            {
                if (!HasAttacked)
                {
                    if (ActualAttackTimer > 0)
                    {
                        ActualAttackTimer -= Time.deltaTime;
                    }
                    else
                    {
                        HasAttacked = true;
                        if (Distance <= MinimumDistance)
                        {
                            PlayerMovement.Push(transform.forward, 10f);
                            PlayerHealth.DamagePlayer(AttackDamage);
                        }
                    }
                }
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        AttackTimer = AttackTime / SpeedModifier;
        HasAttacked = false;
        ActualAttackTimer = ActualAttackTime / SpeedModifier;
        FilthAnimator.Play("Attack");
    }
}
