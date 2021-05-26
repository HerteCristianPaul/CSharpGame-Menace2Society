using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string mainMenuScene;

    public void Resume()
    {
        GameManager.instance.PauseUnpause();                           // Resumes the game
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);                          // Load a specific scene
    }

    public void QuitGame() 
    {
        Application.Quit();                                 // Quits the game
    }
}
