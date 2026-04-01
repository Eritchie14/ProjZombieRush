using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject ZombiePrefab;
    public BoxCollider SpawnZone;
    public int PatrolSize = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: add spawn at certain distance from player
    }

    private void SpawnPatrol()
    {
        
        for (int i = 0; i < PatrolSize; ++i)
        {
            Instantiate(ZombiePrefab, randomSpawn(SpawnZone), Quaternion.identity);
        }
    }

    private Vector3 randomSpawn(BoxCollider box)
    {
        Vector3 localPoint = new Vector3(
            Random.Range(-0.5f, 0.5f),
            0f,
            Random.Range(-0.5f, 0.5f)
        );

        // Convert local to world position, considering rotation & center
        return box.transform.TransformPoint(Vector3.Scale(localPoint, box.size) + box.center);
    }

}
