using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                          // Dependency used for managing scenes

public class CheckpointController : MonoBehaviour
{
    public string cpName;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))                // If there s a value in the playerPrefs
        {  
            // If stored string value in the playerPrefs is = checkpoint name
            if(PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)                  
            {
                PlayerController.instance.transform.position = transform.position;                // Moves the player to this position (checkpoint position)
                Debug.Log("Player starting at " + cpName);                  // Shows us in the console that we have activated the checkpoint
            }      
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))                                                    // By pressing L key
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "");          // We set the playerPrefs to empty
            Debug.Log("Reset");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")                                // Makes sure only the player can trigger the checkpoint
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);     // Stores a string value in playerPrefs | Get current scene name + current checkpoint name
            Debug.Log("Player hit" + cpName);                      // Shows us in the console that we have activated the checkpoint         
            AudioManager.instance.PlaySFX(0);                       // Plays checkpoint sound
        }
    }
}
