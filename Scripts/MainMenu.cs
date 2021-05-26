using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                                  // Library for scene management                 

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string settings;

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);                             // Load a specific scene
    }

    public void SettingsGame()
    {
        SceneManager.LoadScene(settings);                               // Load a specific scene
    }

    public void QuitGame()
    {
        Application.Quit();                                         // Quits the game                                  
    }
}
