using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float requiredMoveSpeed;
    public float moveSpeed;
    public float walkSpeed;
    public float groundDrag;
    public float sprintspeed;
    public float slideSpeed;
    public float WallRunSpeed;
    public ForceMode forceMode;
    private bool isSprinting;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public bool CanMove = true;

    public float stamina;
    public float staminaLeft;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // force sliding when crouching in direction
    [Header("Crouching")]
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool IswalkingOnGround;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Other")]
    public PlayerCam plrCam;

    public Transform orientation;

    public float hozInput;
    public float vertInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        wallrunning,
        air
    }
    public bool sliding;
    public bool crouching; //idfk just incase sliding wont do
    public bool wallrunning;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        startYScale = transform.localScale.y;
        isSprinting = false;
        staminaLeft = stamina;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        if (CanMove)
        {
            
            SpeedControl();
            StateHandler();
        } // All functions after this are passive checkers, they wouldn't necessarily change the movemenet speed variables
        if (grounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
        if(isSprinting)
        {
            if(staminaLeft >0)
            {
                staminaLeft -= Time.deltaTime;
            }
        } else
        {
            if(staminaLeft < stamina)
            {
                staminaLeft += Time.deltaTime;
            }
        }
        staminaLeft = Mathf.Clamp(staminaLeft, 0, stamina);
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            MovePlayer();
        }
         if(hozInput != 0 || vertInput != 0)
        {
            if(grounded)
            {
                IswalkingOnGround = true;
            } else
            {
                IswalkingOnGround = false;
            }
        } else
        {
            IswalkingOnGround = false;
        }

    }
    private void MyInput()
    {
        if (CanMove)
        {
            hozInput = Input.GetAxisRaw("Horizontal");
            vertInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if (Input.GetKeyDown(crouchKey) && grounded)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            if (Input.GetKeyUp(crouchKey) || Input.GetKey(jumpKey))
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
            if (Input.GetKeyDown(sprintKey) && staminaLeft > 0)
            {
                if(hozInput != 0 || vertInput != 0)
                {
                    moveSpeed = sprintspeed;
                    isSprinting = true;
                    plrCam.DoFov(70, 0.9f);
                }
            }
            if (Input.GetKeyUp(sprintKey) || staminaLeft <= 0)
            {
                moveSpeed = requiredMoveSpeed;
                isSprinting = false;
                plrCam.DoFov(60, 1f);
            }
        } else
        {
            if(isSprinting) {
                plrCam.DoFov(60, 1f);
                moveSpeed = requiredMoveSpeed;
                isSprinting = false;
            }
        }
    }
    private void StateHandler()
    {
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = WallRunSpeed;
        }
        else if (sliding)
        {
            state = MovementState.sliding; 

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
            } else
            {
                desiredMoveSpeed = moveSpeed;
            }
        }
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = moveSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = moveSpeed;
        }
        else
        {
            state = MovementState.air;
            desiredMoveSpeed = moveSpeed;
        }

        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
           
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        lastDesiredMoveSpeed = desiredMoveSpeed;
    }
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertInput + orientation.right * hozInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 160f, ForceMode.Force);
            }
        }

        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, forceMode);
        } else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, forceMode);
        }

        if(!wallrunning) rb.useGravity = !OnSlope();
    } 

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        } else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); ;
        //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); -- Optional
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public void Push(Vector3 direction, float pushPower)
    {
        rb.AddForce(direction * pushPower, ForceMode.Impulse);
    }
}
