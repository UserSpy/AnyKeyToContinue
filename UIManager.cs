using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public GameObject controlsMenu;

    public static bool IsGamePaused { get; private set; }

    // Implementing a Singleton pattern for UIManager
    public static UIManager Instance { get; private set; }

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
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

    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        mainMenu.SetActive(IsGamePaused);
        Time.timeScale = IsGamePaused ? 0 : 1;

        if (settingsMenu.activeSelf && !IsGamePaused)
        {
            // Ensures settings menu is closed if game resumes
            settingsMenu.SetActive(false);
        }
        if (controlsMenu.activeSelf && !IsGamePaused)
        {
            // Ensures settings menu is closed if game resumes
            controlsMenu.SetActive(false);
        }
    }
}

