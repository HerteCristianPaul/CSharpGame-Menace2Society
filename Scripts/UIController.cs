using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                           // Dependency for accessing UI elements

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healthText, ammoText;
    public Image damageEffect;                                              // the slightly red ish image
    public float damageAlpha = .25f, damageFadeSpeed = 2f;
    public GameObject pauseScreen;
    public Image blackScreen;
    public float fadeSpeed = 2f;

    public void Awake()                                             // To access this from elsewhere
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageEffect.color.a != 0)                                              // If the alpha from rgb != 0
        {
            // Changes alpha status from damage image | fade in
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));        
        }

        if (!GameManager.instance.levelEnding)
        {               // Changes alpha status from black image | fade in
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        } else
        {               // Changes alpha status from black image | fade out
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, .25f);                   // Changes alpha status from damage image | fades out
    }
}
