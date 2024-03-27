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
    public float FireTime;
    bool ArmExtended = false;
    bool IsExtending = false;
    bool IsRetracting = false;
    float TimePerArm = 0.625f;
    float ArmTimer;

    private IEnumerator RaycastCoroutine;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerHealth = Player.GetComponent<Health>();
        RobotAnimator = RobotPrefab.GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        RaycastCoroutine = CheckRayCast();
        
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
                    RobotAnimator.Play("Arm.Rest");
                    RobotAgent.destination = Player.transform.position;
                    RobotAnimator.Play("Base Layer.Walk");
                } else
                {

                }
            } else
            {
                if (!ArmExtended)
                {
                    RobotAnimator.Play("Base Layer.Idle");
                    RobotAgent.isStopped = true;
                    RobotAnimator.Play("Arm.AimStart");
                    if (!IsExtending && !ArmExtended)
                    {
                        ArmTimer = TimePerArm;
                        IsExtending = true;
                    }
                    if (ArmTimer > 0)
                    {
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

                }
            }
        }
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
                if (Physics.Raycast(RaycastChecker.position, RaycastChecker.forward, out hit, 9999999999f, LayerMask.NameToLayer("Enemy")))
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
