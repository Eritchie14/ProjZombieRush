using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSys : MonoBehaviour
{
    //Gun stats
    public int damage;
    //public float timeBetweenShots, spread, range, reloadTime, timeBetweenShooting;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, BulletsShot=0;

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
        //press R to reload
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            reload();
        }

        //checks to see if player has shot all of their bullets. If so, gun will not shoot until player reloads 
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Shooting = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
         {
            Shooting = false;
        }
        if (Shooting)
        {
            if(BulletsShot >= magazineSize)
            {
                Debug.Log("reload!");
            }
            else{
            Shoot();
            BulletsShot += 1;
            }
        }
        
        
    }

    private void Shoot()
    {
        //method for activating raycast to shoot
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

    private void reload()
    {
        BulletsShot = 0;
    }
}
