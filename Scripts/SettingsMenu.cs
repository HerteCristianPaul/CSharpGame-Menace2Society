using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string mainMenu;

    public void SetVolumeMaster (float masterVolume)                        // Set the volume master(both music & sfx)
    {
        audioMixer.SetFloat("Master Volume", masterVolume);   
    }

    public void SetVolumeMusic (float musicVolume)                      // Set the volume for only music
    {
        audioMixer.SetFloat("Music Volume", musicVolume);   
    }

    public void SetVolumeSFX (float SFXVolume)                          // Set the volume for only effects
    {
        audioMixer.SetFloat("SFX Volume", SFXVolume);   
    }

    public void Back()
    {
        SceneManager.LoadScene(mainMenu);                             // Load the first lvl
    }
}
