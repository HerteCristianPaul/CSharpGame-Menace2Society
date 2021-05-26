using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                      // Library for restart level / respawn

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitAfterDying = 3f;
    [HideInInspector]
    public bool levelEnding;

    private void Awake()                                        
    {
        instance = this;                                // To access this from elsewhere
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;                // Locks cursor - press ESC to show cursor again
        Cursor.visible = false;                                  // Hides cursor
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();                                 // When we hit ESC the PauseMenu pops up
        }
    }

    // Coroutine allows to execute both PlayerDiedCo() and the rest of the code from PlayerHealthController after we called that specific function/method
    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());                                                 
    }

    // Wait for a few seconds | yield = stop doing this function, we don t want to wait for any values to be returned
    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);                               
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);                     // Reload the level when the player dies
    }

    public void PauseUnpause()
    {
         if (UIController.instance.pauseScreen.activeInHierarchy)                       // If the pause screen is active on the screen at the moment
         {
            UIController.instance.pauseScreen.SetActive(false);                         // Deactivate the pause menu
            Cursor.lockState = CursorLockMode.Locked;                                   // Lock the cursor so it s not moving anymore
            Cursor.visible = false;                                                     // Makes the cursou invisible
            Time.timeScale = 1f;                                                        // Restore cursor to normal speed
            PlayerController.instance.footstepFast.Play();                              // Plays footsteps SFX after paused menu usage
            PlayerController.instance.footstepFast.Play();                              // Plays footsteps SFX after paused menu usage
         } 
         else
         {
            UIController.instance.pauseScreen.SetActive(true);                          // Activate the pause menu
            Cursor.lockState = CursorLockMode.None;                                      // Cursor is free to move around
            Cursor.visible = true;                                                      // Makes the cursor visible
            Time.timeScale = 0f;                                                        // Adjust cursor speed
            PlayerController.instance.footstepFast.Stop();                              // Stops the footstep sounds while the game is paused
            PlayerController.instance.footstepSlow.Stop();                              // Stops the footstep sounds while the game is paused

         }
    }
}
