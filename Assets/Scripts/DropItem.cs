using Unity.Mathematics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject ItemToDrop;
    public void Drop()
    {
        Instantiate(ItemToDrop, transform.position + new Vector3(0f, 1f, 0f), quaternion.identity);
    }
}
