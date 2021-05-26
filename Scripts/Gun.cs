using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    [HideInInspector]    // hides in unity inspector
    public float fireCounter;
    public int currentAmmo;
    public Transform firepoint;
    public float zoomAmount;

    // Update is called once per frame
    void Update()
    {
        if(fireCounter > 0)                            
        {
            fireCounter -= Time.deltaTime;                      //  Counting down for automatic fire
        }
    }
}
