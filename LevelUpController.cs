using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUpController : MonoBehaviour
{
    public int gameplayBuild = 2;
    public float levelUpTime = 30f;
    private float timer = 0f;
    public GameObject player;
    public TextMeshProUGUI notice;
    public TextMeshProUGUI left;
    public Button rebindLeft;
    public TextMeshProUGUI right;
    public Button rebindRight;
    public TextMeshProUGUI sprint;
    public Button rebindSprint;
    public TextMeshProUGUI jump;
    public Button rebindJump;
    public Button learnLeft;
    public Button learnRight;
    public Button learnSprint;
    public Button learnJump;
    private bool isRebinding = false;
    private bool isRebindingLeft = false;
    private bool isRebindingRight = false;
    private bool isRebindingSprint = false;
    private bool isRebindingJump = false;
    private KeyCode moveLeft;
    private KeyCode moveRight;
    private KeyCode msprint;
    private KeyCode mjump;
    private bool IsGamePaused = false;
    public GameObject levelUp;
    public void UpdateVisibility()
    {
        rebindLeft.gameObject.SetActive(PlayerPrefs.GetInt("MoveLeftState", 0) == 2);
        rebindRight.gameObject.SetActive(PlayerPrefs.GetInt("MoveRightState", 0) == 2);
        rebindSprint.gameObject.SetActive(PlayerPrefs.GetInt("SprintState", 0) == 2);
        rebindJump.gameObject.SetActive(PlayerPrefs.GetInt("JumpState", 0) == 2);

        learnLeft.gameObject.SetActive(PlayerPrefs.GetInt("MoveLeftState", 0) == 1);
        learnRight.gameObject.SetActive(PlayerPrefs.GetInt("MoveRightState", 0) == 1);
        learnSprint.gameObject.SetActive(PlayerPrefs.GetInt("SprintState", 0) == 1);
        learnJump.gameObject.SetActive(PlayerPrefs.GetInt("JumpState", 0) == 1);


        if (PlayerPrefs.GetInt("MoveLeftState", 0) == 2)
        {
            left.text = "Left: " + (KeyCode)PlayerPrefs.GetInt("MoveLeft", (int)KeyCode.A);
        }
        else if (PlayerPrefs.GetInt("MoveLeftState", 0) == 1)
        {
            left.text = "Left";
        }
        else
        {
            left.text = "";
        }
        if (PlayerPrefs.GetInt("MoveRightState", 0) == 2)
        {
            right.text = "Right: " + (KeyCode)PlayerPrefs.GetInt("MoveRight", (int)KeyCode.A);
        }
        else if (PlayerPrefs.GetInt("MoveRightState", 0) == 1)
        {
            right.text = "Right";
        }
        else
        {
            right.text = "";
        }
        if (PlayerPrefs.GetInt("SprintState", 0) == 2)
        {
            sprint.text = "Sprint: " + (KeyCode)PlayerPrefs.GetInt("Sprint", (int)KeyCode.A);
        }
        else if (PlayerPrefs.GetInt("SprintState", 0) == 1)
        {
            sprint.text = "Sprint";
        }
        else
        {
            sprint.text = "";
        }
        if (PlayerPrefs.GetInt("JumpState", 0) == 2)
        {
            jump.text = "Jump: " + (KeyCode)PlayerPrefs.GetInt("Jump", (int)KeyCode.A);
        }
        else if (PlayerPrefs.GetInt("JumpState", 0) == 1)
        {
            jump.text = "Jump";
        }
        else
        {
            jump.text = "";
        }



    }

    public void LearnMoveLeft()
    {
        int currentState = PlayerPrefs.GetInt("MoveLeftState", 0);
        PlayerPrefs.SetInt("MoveLeftState", currentState + 1);
        PlayerPrefs.Save();
        TogglePause();

    }

    public void LearnMoveRight()
    {
        int currentState = PlayerPrefs.GetInt("MoveRightState", 0);
        PlayerPrefs.SetInt("MoveRightState", currentState + 1);
        PlayerPrefs.Save();
        TogglePause();

    }

    public void LearnSprint()
    {
        int currentState = PlayerPrefs.GetInt("SprintState", 0);
        PlayerPrefs.SetInt("SprintState", currentState + 1);
        PlayerPrefs.Save();
        TogglePause();

    }

    public void LearnJump()
    {
        int currentState = PlayerPrefs.GetInt("JumpState", 0);
        PlayerPrefs.SetInt("JumpState", currentState + 1);
        PlayerPrefs.Save();
        TogglePause();
    }

    public void RebindLeft()
    {
        isRebindingLeft = true;
        isRebinding = true;
        int currentState = PlayerPrefs.GetInt("MoveLeftState", 0);
        PlayerPrefs.SetInt("MoveLeftState", currentState + 1);
    }

    public void RebindRight()
    {
        isRebindingRight = true;
        isRebinding = true;
        int currentState = PlayerPrefs.GetInt("MoveRightState", 0);
        PlayerPrefs.SetInt("MoveRightState", currentState + 1);
    }

    public void RebindSprint()
    {
        isRebindingSprint = true;
        isRebinding = true;
        int currentState = PlayerPrefs.GetInt("SprintState", 0);
        PlayerPrefs.SetInt("SprintState", currentState + 1);
    }

    public void RebindJump()
    {
        isRebindingJump = true;
        isRebinding = true;
        int currentState = PlayerPrefs.GetInt("JumpState", 0);
        PlayerPrefs.SetInt("JumpState", currentState + 1);
    }

    void LoadControls()
    {
        moveLeft = (KeyCode)PlayerPrefs.GetInt("MoveLeft", (int)KeyCode.A);
        moveRight = (KeyCode)PlayerPrefs.GetInt("MoveRight", (int)KeyCode.D);
        msprint = (KeyCode)PlayerPrefs.GetInt("Sprint", (int)KeyCode.LeftShift);
        mjump = (KeyCode)PlayerPrefs.GetInt("Jump", (int)KeyCode.Space);
    }

    void SaveControls()
    {
        PlayerPrefs.SetInt("MoveLeft", (int)moveLeft);
        PlayerPrefs.SetInt("MoveRight", (int)moveRight);
        PlayerPrefs.SetInt("Sprint", (int)msprint);
        PlayerPrefs.SetInt("Jump", (int)mjump);
        PlayerPrefs.Save();
    }

    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        levelUp.SetActive(IsGamePaused);
        Time.timeScale = IsGamePaused ? 0 : 1;
    }

    private void Update()
    {
        if (isRebinding)
        {
            LoadControls();
            notice.text = "Press Any Key To Rebind";
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode != KeyCode.Escape)
                    {
                        if (isRebindingLeft)
                        {
                            moveLeft = keyCode;
                            isRebindingLeft = false;
                        }
                        if (isRebindingRight)
                        {
                            moveRight = keyCode;
                            isRebindingRight = false;
                        }
                        if (isRebindingSprint)
                        {
                            msprint = keyCode;
                            isRebindingSprint = false;
                        }
                        if (isRebindingJump)
                        {
                            mjump = keyCode;
                            isRebindingJump = false;
                        }
                        SaveControls();
                        player.GetComponent<PlayerMovement>().LoadControls();
                        UpdateVisibility();
                        isRebinding = false;
                        notice.text = "";
                        TogglePause();
                        break;
                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == gameplayBuild && !IsGamePaused)
        {
            timer += Time.deltaTime; // Increment the timer by the time since the last frame.
        }

        if (timer >= levelUpTime)// Check if time has passed
        {
            TogglePause();
            UpdateVisibility();
            timer = 0f;
        }
    }


}
