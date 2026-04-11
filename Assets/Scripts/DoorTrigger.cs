using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorManager door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();
        }
    }
}
