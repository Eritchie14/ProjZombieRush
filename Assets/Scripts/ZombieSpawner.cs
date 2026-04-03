using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject ZombiePrefab;
    private BoxCollider SpawnZone;
    [Range(0,50)] [SerializeField] float SpawnRange = 5;
    public int PatrolSize = 5;
    private GameObject Player;
    private float distance;
    private int spawnCount = 0;
   void Awake() {
    if (SpawnZone == null)
        SpawnZone = GetComponent<BoxCollider>();

    if (SpawnZone == null)
        Debug.LogError("No BoxCollider on this GameObject!");
}
    void Start()
    {
        //SpawnPatrol();
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player != null)
        {
            distance = Vector3.Distance(SpawnZone.transform.position, Player.transform.position);
            //Debug.Log(distance);
        }
        else
        {
            Debug.LogError("No object of Player Exists in scene");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: add spawn at certain distance from player
        distance = Vector3.Distance(SpawnZone.transform.position, Player.transform.position);//checks how far player is from zone
        if (distance <= SpawnRange && spawnCount < 1) //will spawn patrol if player goes into spawn boundary
        {
            SpawnPatrol();
            spawnCount++;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, SpawnRange);
    }

}
