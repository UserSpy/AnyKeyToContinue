using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStart : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public GameObject controlsMenu;


    // Implementing a Singleton pattern for UIManager
    public static UIManagerStart Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ToggleSettingsMenu()
    {
        bool isSettingsMenuOpen = settingsMenu.activeSelf;
        mainMenu.SetActive(isSettingsMenuOpen);
        settingsMenu.SetActive(!isSettingsMenuOpen);
    }

    public void ToggleControlsMenu()
    {
        bool isControlsMenuOpen = controlsMenu.activeSelf;
        // Toggle the visibility of the menus.
        settingsMenu.SetActive(isControlsMenuOpen);
        controlsMenu.SetActive(!isControlsMenuOpen);
    }
}

