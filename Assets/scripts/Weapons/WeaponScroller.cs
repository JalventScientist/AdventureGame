using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScroller : MonoBehaviour
{
    public GameObject[] arsenal; 
    /* 
    0 = Revolver
    1 = Shotgun
    */
    private int currentWeapon = 0;
    private int MaxWeapon;
    private float WeaponChangeTimer;
    public float WeaponChangeTime = 0.2f;
    public KeyCode[] WeaponKeys;
    /*
    1 = Revolver
    2 = Shotgun
    */
    public Vector3[] GunPositions;
    private KeyCode ActiveKey;
    private GameObject CurrentWeaponObject;
    public bool ActuallyDoesStuff = true;

    private void Start()
    {
        MaxWeapon = arsenal.Length -1;
        CurrentWeaponObject = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(ActuallyDoesStuff)
        {

            if (WeaponChangeTimer > 0f)
            {
                WeaponChangeTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.mouseScrollDelta.y > 0f) // forward
                {
                    int NextWeapon = currentWeapon + 1;
                    if (NextWeapon > MaxWeapon)
                    {
                        NextWeapon = 0;
                    }
                    ChangeWeapon(NextWeapon);
                }
                if (Input.mouseScrollDelta.y < 0f)
                {
                    int NextWeapon = currentWeapon - 1;
                    if (NextWeapon < 0)
                    {
                        NextWeapon = MaxWeapon;
                    }
                    ChangeWeapon(NextWeapon);
                }
            }


        }
    }

    public void ChangeWeapon(int WeaponChoice)
    {
        Destroy(CurrentWeaponObject);
        GameObject newGun = Instantiate(arsenal[WeaponChoice]);
        ActiveKey = WeaponKeys[WeaponChoice];
        newGun.transform.parent = transform;
        newGun.transform.localRotation = new Quaternion(0, 0, 0, 0);
        newGun.transform.localPosition = GunPositions[WeaponChoice];
        CurrentWeaponObject = newGun;
        currentWeapon = WeaponChoice;
        WeaponChangeTimer = WeaponChangeTime;
    }
}
