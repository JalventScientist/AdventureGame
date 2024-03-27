using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyRobotBehave : MonoBehaviour
{
    [Header("Activation")]
    public bool BehaviourEnabled;

    [Header("References")]
    public Animator RobotAnimator;
    public GameObject RobotPrefab;
    public NavMeshAgent RobotAgent;
    public Transform RaycastChecker;
    private GameObject Player;
    private Health PlayerHealth;
    private PlayerMovement PlayerMovement;

    public float RayCastDelay;
    private bool SuccessfulCast;

    private float FireTimer;
    public float FireTime = 2f;
    bool ArmExtended = false;
    bool IsExtending = false;
    bool IsRetracting = false;
    float TimePerArm = 0.625f;
    float ArmTimer;
    bool CanFire = false;
    bool IsCounting = false;
    private IEnumerator RaycastCoroutine;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerHealth = Player.GetComponent<Health>();
        RobotAnimator = RobotPrefab.GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        RaycastCoroutine = CheckRayCast();
        StartCoroutine(RaycastCoroutine);
    }

    int LayerMasks; // I have no idea what this does but oh well
    private void Update()
    {
        if (BehaviourEnabled)
        {
            if(!SuccessfulCast)
            {
                if(ArmExtended)
                {
                    
                    if (!IsRetracting)
                    {
                        ArmTimer = TimePerArm;
                        IsRetracting = true;
                        RobotAnimator.Play("Arm.AimEnd");
                    } else
                    {
                        
                        if (ArmTimer > 0)
                            ArmTimer -= Time.deltaTime;
                        else
                        {
                            IsRetracting = false;
                            ArmExtended = false;
                        }
                    }
                }
                if (!IsRetracting && !ArmExtended)
                {
                    RobotAgent.isStopped = false;
                    RobotAnimator.Play("Rest", 1);
                    RobotAgent.destination = Player.transform.position;
                    RobotAnimator.Play("Walk", 0);
                } else
                {

                }
            } else
            {
                if (!ArmExtended)
                {
                    RobotAnimator.Play("Idle", 0);
                    RobotAgent.isStopped = true;
                    RobotAnimator.Play("AimStart", 1);
                    if (!IsExtending && !ArmExtended)
                    {
                        ArmTimer = TimePerArm;
                        IsExtending = true;
                    }
                    
                    if (ArmTimer > 0 && IsExtending)
                    {
                        print(ArmTimer);
                        ArmTimer -= Time.deltaTime;
                    }
                    else
                    {
                        ArmExtended = true;
                        IsExtending = false;
                    }
                }
                if(ArmExtended)
                {
                    FireTimer = FireTime;
                    IsCounting = true;
                    if (IsCounting)
                    {
                        if (FireTimer > 0)
                        {
                            FireTimer -= Time.deltaTime;
                        }
                        else
                        {
                            FireTimer = FireTime;
                            print("Fired");
                        }
                    }

                }
            }
        }
    }

    IEnumerator FireCannon()
    {
        yield return new WaitForSeconds(0.625f);
    }

    IEnumerator CheckRayCast()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(RayCastDelay);
            if(!IsRetracting && !IsExtending)
            {
                RaycastHit hit;
                RaycastChecker.LookAt(Player.transform);
                if (Physics.Raycast(RaycastChecker.position, RaycastChecker.forward, out hit, 100f, LayerMask.NameToLayer("Enemy")))
                {
                    if (hit.collider.gameObject.tag == "PlayerCollider")
                    {
                        SuccessfulCast = true;
                    }
                    else
                    {
                        SuccessfulCast = false;
                    }
                }
                else
                {
                    SuccessfulCast = false;
                }
            }
        }
    }
}
