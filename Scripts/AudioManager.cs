using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgm;
    public AudioSource victory;
    public AudioSource[] soundEffects;
    public AudioSource[] stepsBugFix;

    public void Awake() 
    {
        instance = this;                        // Makes this script accessible from another
    }

    public void StopBGM()
    {
        bgm.Stop();                                 // Stops background music
    }

    public void PlayLevelVictory()
    {   
        StopBGM();                                  // Stops the background music
        victory.Play();                             // Plays the victory sound
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();               // This way the sfx we wanna play won t play over an already playing sfx (same sfx)
        soundEffects[sfxNumber].Play();
    }

    public void StopSFX(int sfxNumber1)
    {
        soundEffects[sfxNumber1].Stop();                    // Stops a specific sfx
    }

    public void StopSFXSteps(int sfxNumber1)                     // Stops a specific sfx(steps) | for fixing a bug
    {                  
        stepsBugFix[sfxNumber1].Stop();
    }
}