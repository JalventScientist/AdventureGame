using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float maxWallRunTime;
    public float wallClimbSpeed;
    private float wallRunTimer;
    private float exitwallTimer;
    public float exitWallTime;
    private bool exitingWall;
    public float HowManyWalljumps;
    private float WallJumpCounter;
    private bool ReachedMax;

    [Header("Input")]
    public KeyCode wallJumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsrunning;
    private bool downwardsrunning;
    private float hozInput;
    private float vertInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    public PlayerCam cam;
    public Camera ActualCam;
    private PlayerMovement pm;
    private Rigidbody rb;

    public float DefaultFov;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        ReachedMax = false;
    }
    private void Update()
    {
        CheckForWall();
        Statemachine();
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning)
        {
            WallRunningMovement();
        }
        if (pm.grounded)
        {
            WallJumpCounter = 0;
        }
        if (WallJumpCounter >= HowManyWalljumps)
        {
            ReachedMax = true;
        }
        else
        {
            ReachedMax = false;
        }
    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void Statemachine()
    {
        hozInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        upwardsrunning = Input.GetKey(upwardsRunKey);
        downwardsrunning = Input.GetKey(downwardsRunKey);

        if((wallLeft || wallRight) && vertInput >0 && AboveGround() && !exitingWall)
        {
            if (!pm.wallrunning)
            {
                if(!ReachedMax)
                {
                    StartWallRun();
                }
            }
            if (wallRunTimer > 0)
                wallRunTimer -= Time.deltaTime;
            if(wallRunTimer <= 0 && pm.wallrunning)
            {
                WallJumpCounter += 1;
                ForceOffWall();
            }
            if (Input.GetKeyDown(wallJumpKey) && !ReachedMax)
            {
                WallJumpCounter += 1;
                WallJump();
            }
        }


        else if (exitingWall)
        {
            if (pm.wallrunning)
            {
                WallJumpCounter += 1;
                StopWallRun();
            }

            if (exitwallTimer > 0)
                exitwallTimer -= Time.deltaTime;
            if (exitwallTimer <= 0)
            {
                exitingWall = false;
            }
           
        }
        else
        {
            if (pm.wallrunning)
            {
                WallJumpCounter += 1;
                StopWallRun();
            }
                
        }
    }

    private void StartWallRun()
    {
        pm.wallrunning = true;
        wallRunTimer = maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //apply cam fx
        cam.DoFov(DefaultFov * 1.1f);
        if (wallLeft) cam.DoTilt(-5f);
        if (wallRight) cam.DoTilt(5f);
    }
    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (upwardsrunning)
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        if (downwardsrunning)
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        if (!(wallLeft && hozInput >0) && !(wallRight && hozInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if(useGravity)
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
    }
    private void StopWallRun()
    {
        pm.wallrunning = false;

        cam.DoFov(DefaultFov);
        cam.DoTilt(0f);
    }

    private void WallJump()
    {

        exitingWall = true;
        exitwallTimer = exitWallTime;
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
    private void ForceOffWall()
    {
        exitingWall = true;
        exitwallTimer = exitWallTime;
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
