using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    public float walkingBobbingSpeed;
    public float bobbingAmount;
    public PlayerMovement Plr;
    public Rigidbody rb;

    float defaultPosY = 0f;
    float timer = 0f;

    private void Start()
    {
        defaultPosY = transform.localPosition.y;

    }

    private void Update()
    {
        if(Plr.IswalkingOnGround || Plr.wallrunning)
        {
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        } else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
