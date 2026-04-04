using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    
    public string clipSize = "0/0";
    //bool empty = true;
    //public int ammo = 0;

    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Healthnum;
    public Image HealthBar;
    public Image Key;
    public bool hasKey;

    public GameObject GameOverPanel;

    void Start()
    {
        GameOverPanel.SetActive(false);
        hasKey = false;
    }
    void Update()
    {
        if (hasKey)
        {
            getKey();
        }
    }

    public void DecreaseAmmo()
    {
        
    }

    public void setAmmo(int clipSize, int numClips)
    {
        Ammo.text = clipSize + "/" + numClips;
    }
    public void updateAmmo(int bullets, int numClips)
    {
        Ammo.text = bullets + "/" + numClips;
    }

    public void updateHealth(float Health)
    {
        HealthBar.fillAmount = Health;
    }

    public void PlayerDeath()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void getKey()
    {
        Key.color = Color.gold;
    }

}
