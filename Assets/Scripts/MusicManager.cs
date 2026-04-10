using UnityEngine;

public class MusicManager : MonoBehaviour
{

    HealthSys PlayerHealth;
    GameObject Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = Player.transform.Find("HealthSystem").GetComponent<HealthSys>();

    }

    void Start()
    {
        GameObject.FindGameObjectsWithTag("Player");
         AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music_Menu);
    }

    void Update()
    {
        if (PlayerHealth.isDead())
        {
            AudioManager.Instance.StopMusic();
        }
    }
}
