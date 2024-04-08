using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RevolverFire : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    private bool CanFire;
    public float ReloadTime = 1.7f;
    private float ReloadTimer;
    public float maxDistance = 90f;
    public float Damage;
    public LayerMask WhatIsEnemy;
    public LayerMask EnemyTrigger;
    public Camera orientation;
    public GameObject RevolverBulletLine;
    public GameObject Blood;
    public bloodEmitter BloodParticles;
    public GameObject StartPos;
    public GameObject HitPos;
    private RevolverTrail TrailScript;
    private ScoreSystem ScoreSystem;
    int LayerMasks;

    /* BUNDLE
;

ScoreSystem.AddScore(Random.Range(250, 300)); // ADDS SCORE
ScoreSystem.NewKill(); // ON KILL
 */
    private void Start()
    {
        orientation = GameObject.FindWithTag("PlrCam").GetComponent<Camera>();
        BloodParticles = Blood.GetComponent<bloodEmitter>();
        TrailScript = RevolverBulletLine.GetComponent<RevolverTrail>();
        int AllLayers = Physics.AllLayers;
        int LayerToExclude = LayerMask.GetMask("Trigger");
        LayerMasks = AllLayers & ~LayerToExclude;
        ScoreSystem = GameObject.FindWithTag("Score").GetComponent<ScoreSystem>();
    }
    public void Fire()
    {
        Vector3 fwd = orientation.transform.TransformDirection(Vector3.forward);
        RaycastHit hitenemy;
        if (Physics.Raycast(orientation.transform.position, orientation.transform.forward, out hitenemy, 999999999999999999f, LayerMasks))
        {
            HitPos.transform.position = hitenemy.point;
            float Distance = Vector3.Distance(orientation.transform.position, hitenemy.point);
            TrailScript.setTrail(Distance);
            if (hitenemy.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                float Score = Random.Range(100, 125);
                ScoreSystem.AddScore(Score);
                GameObject THEBLOOD = Instantiate(Blood);
                THEBLOOD.GetComponent<bloodEmitter>().AttachToObject(hitenemy.collider.transform.position);
                hitenemy.transform.gameObject.GetComponent<EnemyHealth>().health -= Damage;
            }
        }
    }
}
