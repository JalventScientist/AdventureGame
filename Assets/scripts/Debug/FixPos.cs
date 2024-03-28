using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPos : MonoBehaviour
{

    public Transform PositionToResetTo;
    //THIS SCRIPT ONLY FIXES THE POSITION OF THE PLAYER IF IT HITS;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerCollider") {
            collision.transform.parent.transform.position = PositionToResetTo.position;
        }
    }
}
