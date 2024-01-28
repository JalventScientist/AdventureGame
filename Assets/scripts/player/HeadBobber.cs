using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    public float walkingBobbingSpeed;
    public float bobbingAmount;
    public GameObject PlayerBody;
    public Rigidbody rb;

    float defaultPosY = 0f;
    float timer = 0f;

    private void Start()
    {
        defaultPosY = transform.localPosition.y;

    }

    private void Update()
    {
        if(rb.velocity.x > 0.1f || rb.velocity.x > 0.1f)
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
