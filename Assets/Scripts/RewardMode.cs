using Unity.Mathematics;
using UnityEngine;

public class RewardMode : MonoBehaviour
{
   public GameObject meteor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void spawnMeteor(GameObject meteor)
    {
        Instantiate(meteor, transform.position, quaternion.identity);
        Destroy(meteor, 5f);
    }
}
