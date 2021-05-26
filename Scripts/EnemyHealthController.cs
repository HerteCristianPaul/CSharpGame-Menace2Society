using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5;

    // Handle Damage
    public void DamageEnemy(int damageAmount)
    {
        AudioManager.instance.PlaySFX(3);                               // Plays player-hurt sound effect
        currentHealth -= damageAmount;                                  // Recalculating health after the damage taken
        if (currentHealth <= 0)                                     // If the player has life <= 0 
        {                               
            Destroy(gameObject);                                    // Player dies
            AudioManager.instance.PlaySFX(2);                       // Play player-dies sound effect
        }
    }
}
