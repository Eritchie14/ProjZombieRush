using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSys : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShots, spread, range, reloadTime, timeBetweenShooting;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, BulletsShot;

    //bools
    bool Shooting, readyToshoot, reloading;

    //References
    //public Camera fpsCam;
    // public Transform attackpoint;
    //public RaycastHit rayHit;
    //public LayerMask WhatIsEnemy;

    public GameObject ImpactParticle;
    GameObject impact;
    void Update()
    {
        //system for if player clicks left mouse, player will pull trigger and hit/miss something
        if(Mouse.current.leftButton.wasPressedThisFrame){
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 20f))
        {
            Debug.Log("Hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*hitInfo.distance, Color.red);

            //particle effect on hit. to be destroyed after hit
            impact = Instantiate(ImpactParticle, hitInfo.point, quaternion.identity);
            Destroy(impact, 2f);
            impact = null;

        }
        else
        {
            Debug.Log("Miss");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*20f, Color.green);
        }
        }
    }
}
