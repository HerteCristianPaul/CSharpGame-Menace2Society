using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string mainMenuScene;
    public float timeBeforeShowing = 1f;
    public GameObject returnButton, textBox;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowObjectCo());                                         // Calls method from below
        Cursor.lockState = CursorLockMode.None;                                 // Stops the cursor from locking 
        Cursor.visible = true;                                                  // Cursor is visible now
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);                          // Loads a specific scene
    }

    public IEnumerator ShowObjectCo()
    {
        yield return new WaitForSeconds(timeBeforeShowing);                 // Wait an amount of time (timeBeforeShowing)
        textBox.SetActive(true);                                            // To display the text
        yield return new WaitForSeconds(timeBeforeShowing);                 // Wait an amount of time (timeBeforeShowing)
        returnButton.SetActive(true);                                       // To display the button
    }
}
