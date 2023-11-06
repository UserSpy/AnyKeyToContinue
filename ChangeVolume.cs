using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    private float volumeLevel;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("GameVolume"))
        {
           
            volumeLevel = 1.0f; // 1.0f represents full volume.
            volumeSlider.value = volumeLevel; // Update the slider to match the default volume.
        }
        else
        {
            // If there's a player preference, load it and update the slider accordingly.
            volumeLevel = PlayerPrefs.GetFloat("GameVolume");
            volumeSlider.value = volumeLevel;
        }

        // Add a listener to the slider to respond to volume changes.
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float newVolume)
    {
        volumeLevel = newVolume;
        PlayerPrefs.SetFloat("GameVolume", volumeLevel);
        PlayerPrefs.Save();
    }
}
