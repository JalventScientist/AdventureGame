using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyRobotBehave : MonoBehaviour
{
    [Header("Activation")]
    public bool BehaviourEnabled;

    [Header("References")]
    private Animator RobotAnimator;
    public Transform ModelHolder;
    public GameObject RobotPrefab;
    public NavMeshAgent RobotAgent;
    public Transform RaycastChecker;
    private GameObject Player;
    private Health PlayerHealth;
    private PlayerMovement PlayerMovement;
    [Header("Projectile")]
    public GameObject Projectile;
    public GameObject Curve;
    private QuadraticCurve CurveScript;
    public Transform CurvePointA;
    public Transform CurvePointB;
    public Transform ControlPoint;

    [Header("Other")]
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
        CurveScript = Curve.GetComponent<QuadraticCurve>();
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
                        StartCoroutine(RetractArm());
                        IsRetracting = true;
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
                    
                    if (!IsExtending && !ArmExtended)
                    {
                        StartCoroutine(ExtendArm());
                        IsExtending = true;
                    }
                }
                if(ArmExtended)
                {
                    if (!IsCounting)
                    {
                        StartCoroutine(WaitForFire());
                    }

                }
            }
        }
    }

    IEnumerator ExtendArm()
    {
        RobotAnimator.Play("AimStart", 1);
        yield return new WaitForSeconds(TimePerArm);
        IsExtending = false;
        ArmExtended = true;
    }

    IEnumerator RetractArm()
    {
        RobotAnimator.Play("AimEnd", 1);
        yield return new WaitForSeconds(TimePerArm);
        IsRetracting = false;
        ArmExtended = false;
    }

    IEnumerator WaitForFire()
    {
        IsCounting = true;
        yield return new WaitForSeconds(FireTime);
        if(!SuccessfulCast) {
            IsCounting = false;
            yield break;
        } else
        {
            RobotAnimator.Play("Fire", 1);
            ModelHolder.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
            CurvePointB.position = Player.transform.position;
            float Distance = Vector3.Distance(CurvePointA.position, CurvePointB.position);
            float HeightModifier = 2 + (Distance / 10); // Sets Curve height
            ControlPoint.position = Vector3.Lerp(CurvePointA.position, CurvePointB.position, 0.5f);
            ControlPoint.position = new Vector3(ControlPoint.position.x, ControlPoint.position.y + HeightModifier, ControlPoint.position.z);
            GameObject Bullet = Instantiate(Projectile, CurvePointA.position, CurvePointA.rotation);
            Bullet.transform.parent = null;
            Bullet.GetComponent<RobotProjectile>().curve = CurveScript;
            yield return new WaitForSeconds(0.625f);
            IsCounting = false;
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
