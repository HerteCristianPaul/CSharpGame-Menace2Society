using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string nextLevel;
    public float waitToEndLevel;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            GameManager.instance.levelEnding = true;                   // The player has completed the objective so the level can end
            StartCoroutine(EndLevelCo());                              // Call the method below
            AudioManager.instance.PlayLevelVictory();                  // Plays victory music
            AudioManager.instance.StopSFXSteps(0);                     // Plays a specific sound effect
            AudioManager.instance.StopSFXSteps(1);                     // Plays a specific sound effect
        }
    }

    private IEnumerator EndLevelCo()
    {
        PlayerPrefs.SetString(nextLevel + " _cp", "");                  // Sets the playerPrefs to empty | so we start at beginning after we finish the level 
        yield return new WaitForSeconds(waitToEndLevel);                // Waits a certain amount of time till the scene changes
        SceneManager.LoadScene(nextLevel);                              // Loads a specific scene (nextLevel)
    }
}
