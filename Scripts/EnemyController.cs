using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;                   // Dependency for NPC

public class EnemyController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;
    private Vector3 targetPoint, startPoint;
    public NavMeshAgent agent;
    public float keepChasingTime = 5f;
    private float chaseCounter;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;
    public Animator anim;

    void Start()
    {
        startPoint = transform.position;                                            // Sets the startPoint to the current position of the enemy
        shootTimeCounter = timeToShoot;                                             // Sets the time to shoot counter
        shotWaitCounter = waitBetweenShots;                                         // Sets the wait between shots counter 
    }

    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;                           // Sets the target point as the player
        targetPoint.y = transform.position.y;                                                 // The enemy won t look up & down | target = same y position
        if (chasing == false)                                                                 // If the enemy is not chasing the player
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase)           // If the distance from the player-->enemy < the distance that the enemy would start chasing
            {
                chasing = true;                                                               // Sets chasing boolean variable to true
                shootTimeCounter = timeToShoot;                                               // Sets the time to shoot counter
                shotWaitCounter = waitBetweenShots;                                           // Sets the wait between shots counter | Wait 1sec till the first blast
            }

            if (chaseCounter > 0)                                                           // If the chase counter > 0
            {
                chaseCounter -= Time.deltaTime;                                             // Chase counter decrement
                if (chaseCounter <= 0)                                                      // If the chase counter <= 0
                {
                    agent.destination = startPoint;                                         // After chasing the player walks back to initial position
                }
            }

            if (agent.remainingDistance < .25f)                                             // If the enemy is very close to the destination
            {
                anim.SetBool("isMoving", false);                                            // The enemy stops moving
            }
            else
            {
                anim.SetBool("isMoving", true);                                             // The enemy is still moving

            }
        } 
        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)           // If the distance to the target > distance to stop chasing
            {
                agent.destination = targetPoint;                                              // Moving towards the player(target)
            }
            else
            {
                agent.destination = transform.position;                                       // The enemy stops where he is (in case is very close) so he s not touching the player
            }                                                            

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)           // Distance from the player-->enemy > distance to stop chasing
            {
                chasing = false;                                                              // Sets the chasing boolean to false
                chaseCounter = keepChasingTime;                                               // Sets the chase counter so the enemy is chasing for at least 5 sec
            }

            // Makes the enemy wait for 2 sec between the shots
            if (shotWaitCounter > 0)                                               
            {
                shotWaitCounter -= Time.deltaTime;                                  // Countdown with decrement

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);                                     // The enemy is still moving
            }
            else
            {
                if (PlayerController.instance.gameObject.activeInHierarchy)                                        // If the player is still active in the scene
                {
                    shootTimeCounter -= Time.deltaTime;                                                         // Shoot counter decrement
                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;                                                                    // Countdown counter decrement
                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;
                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));      // Enemy will fire at player even if he jumps

                            //check the angle to the player
                            Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);                       // Stores the angle amount 

                            if (Mathf.Abs(angle) < 30f)                                                // Math.Abs always returns a positive (3 = 3; -3 = 3) | if the angle is < 30 degrees
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);                                // Creates the bullet and give direction | enemy --> player  
                                anim.SetTrigger("fireShot");                                                                // Play fire animation
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }

                        }
                        agent.destination = transform.position;                                                          // Enemy doesn t move while fire
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }
                    anim.SetBool("isMoving", true);                                     // The enemy is moving
            }
        }
    }
}
