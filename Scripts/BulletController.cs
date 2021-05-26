using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody theRB;
    public GameObject impactEffect;
    public int damage = 1;
    public bool damageEnemy, damagePlayer;

    // Update is called once per frame
    public void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;                         // Makes the bullet move
        lifeTime -= Time.deltaTime;                         // Bullet lifetime decrement
        if (lifeTime <= 0)
        {
            Destroy(gameObject);                // Destroys the bullet when its life is below 0
        }
    }

    // colider on colider trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy == true)                              // If the object has the enemy tag
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);       // Deals damage when it is not a headshot
        }

        // Headshot = double damage
        if (other.gameObject.tag == "Headshot" && damageEnemy)                  // If the object has the headshot tag
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 2);                   // Deals double damage when it is a headshot
        }
            
        if (other.gameObject.tag == "Player" && damagePlayer == true)                       // If the object is the player deal damage
        {
            PlayerHealthController.instance.DamagePlayer(damage);                           // If the target is the player deal damage
        }

        Destroy(gameObject);                                                        // Destroys the bullet                                             
        Instantiate(impactEffect, transform.position, transform.rotation);                    // Impact effect appears where & after the bullet gets destroyed
    }
}