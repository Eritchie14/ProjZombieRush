using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSys : MonoBehaviour
{
    //Gun stats
    public int damage;
    public int magazineSize, bulletsPerTap, numClips;
    int BulletsShot=0; int bulletsLeft=0;

    //bools
    bool Shooting;

    //References
     public Transform attackpoint;
     public Hud hud;
    //public RaycastHit rayHit;
    //public LayerMask WhatIsEnemy;

    public GameObject ImpactParticle;
    public GameObject muzzleFlashParticle;
    GameObject impact;
    public GameObject firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    void Start()
    {
        bulletsLeft = magazineSize;
        hud.updateAmmo(bulletsLeft, numClips);
    }
    void Update()
    {
        //press R to reload
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if(numClips > 0 && bulletsLeft != magazineSize){
            reload();
            bulletsLeft = magazineSize;
            numClips -= 1;
            hud.updateAmmo(bulletsLeft, numClips);
            }
            else
            {
                Debug.Log("no more ammo!");
            }
        }

        //checks to see if player has shot all of their bullets. If so, gun will not shoot until player reloads 
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Shooting = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
         {
            Shooting = false;
        }
        //checks to see if player should be shooting and how fast to shoot
        if (Shooting && Time.time > nextFireTime)
        {
            if(BulletsShot >= magazineSize)
            {
                Debug.Log("reload!");
            }
            else{
                Shoot();
                nextFireTime = Time.time + fireRate;
                BulletsShot += 1;
                bulletsLeft -= 1;
                //hud update
                hud.updateAmmo(bulletsLeft, numClips);
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

            //TODO: Needs fixing, Muzzle flash is lagging behind attack point. needs to move with the attack point 
            //  firePoint = Instantiate(muzzleFlashParticle, attackpoint.position, quaternion.identity);
            //  Destroy(firePoint, 0.2f);
            //  firePoint = null;

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
