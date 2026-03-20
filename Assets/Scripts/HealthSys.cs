using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSys : MonoBehaviour
{
    public float maxHealth;
    float CurrentHealth;
    public float damage;
    bool playerDead;

    //refernces
    public Hud hud;
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: change to when enemy collides or something
        //what happens when player gets damaged
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
            hud.updateHealth(CurrentHealth/maxHealth);
            if(CurrentHealth <= 0)
            {
                hud.PlayerDeath();
            }
        }
        
    }

}
