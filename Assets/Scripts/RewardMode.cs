using Unity.Mathematics;
using UnityEngine;

public class RewardMode : MonoBehaviour
{
   public GameObject meteor;
   public float HealthBoost;
   public int GiveAmmoAmount;
   private GameObject Player;
   private HealthSys PlayerHealth;
   private GunSys PlayerGun;


    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HealthBoost = 25f;
        GiveAmmoAmount = 3;
    }
    private void OnTriggerEnter(Collider other) //detects what item player has collided with
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("Health"))
            {
                PlayerHealth = Player.transform.Find("HealthSystem").GetComponent<HealthSys>();

                if(PlayerHealth.CurrentHealth == PlayerHealth.maxHealth)
                {
                    Debug.Log("At full Health");
                }
                else{

                PlayerHealth.HealPlayer(HealthBoost);
                //Debug.Log("Picked up Health");
                Destroy(transform.gameObject);
                }
            }
        }
        if (CompareTag("Ammo"))
        {
            PlayerGun = Player.transform.Find("PlayerCameraRoot").transform.Find("Main Camera").GetComponent<GunSys>();
                PlayerGun.getAmmo(GiveAmmoAmount);
                Destroy(transform.gameObject);
                Debug.Log("Picked up ammo");
        }    
         //Debug.Log("hit: " + this.tag);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void spawnMeteor(GameObject meteor)
    {
        Instantiate(meteor, transform.position, quaternion.identity);
        Destroy(meteor, 5f);
    }
}
