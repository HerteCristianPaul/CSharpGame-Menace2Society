using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;                                               
    public int maxHealth, currentHealth;

    private void Awake()
    {
        instance = this;                                                                        // To access this from elsewhere
    }

    void Start()
    {
        currentHealth = maxHealth;                                                              // When we start we have full health
        UIController.instance.healthSlider.maxValue = maxHealth;                                // Get maxHealth value | Always display the health slidebar length after maxHealth
        UIController.instance.healthSlider.value = currentHealth;                               // Get currentHealth value
        UIController.instance.healthText.text = "HEALTH: " + currentHealth;                     // Update Healthbar on start
    }

    public void DamagePlayer(int damageAmount)                                                  // Allows us to take dammage
    {
        AudioManager.instance.PlaySFX(3);                      
        currentHealth -= damageAmount;                                                          // Recalculating health after damage taken | taking damage
        UIController.instance.ShowDamage();                                                     // When player takes damage 

        if (currentHealth <= 0)                                                                 // The player dies if life is 0 or below 
        {
            gameObject.SetActive(false);
            currentHealth = 0;                                                              // So we don t display nagative values
            GameManager.instance.PlayerDied();           
            AudioManager.instance.StopBGM();   
            AudioManager.instance.StopSFX(3); 
            AudioManager.instance.PlaySFX(2);                      
        }
        UIController.instance.healthSlider.value = currentHealth;                               // Get currentHealth value
        UIController.instance.healthText.text = "HEALTH: " + currentHealth;                     // Update Healthbar every time he takes damage
    }
}
