using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlsManager : MonoBehaviour
{
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
    private bool isRebinding = false;
    private bool isRebindingLeft = false;
    private bool isRebindingRight = false;
    private bool isRebindingSprint = false;
    private bool isRebindingJump = false;
    private KeyCode moveLeft;
    private KeyCode moveRight;
    private KeyCode msprint;
    private KeyCode mjump;
    // Start is called before the first frame update
    public void UpdateVisibility()
    {
        rebindLeft.gameObject.SetActive(PlayerPrefs.GetInt("MoveLeftState", 0) == 3);
        rebindRight.gameObject.SetActive(PlayerPrefs.GetInt("MoveRightState", 0) == 3);
        rebindSprint.gameObject.SetActive(PlayerPrefs.GetInt("SprintState", 0) == 3);
        rebindJump.gameObject.SetActive(PlayerPrefs.GetInt("JumpState", 0) == 3);

        if (PlayerPrefs.GetInt("MoveLeftState", 0) > 1)
        {
            left.text = "Left: " + (KeyCode)PlayerPrefs.GetInt("MoveLeft", (int)KeyCode.A);
        }
        else
        {
            left.text = "";
        }
        if (PlayerPrefs.GetInt("MoveRightState", 0) > 1)
        {
            right.text = "Right: " + (KeyCode)PlayerPrefs.GetInt("MoveRight", (int)KeyCode.A);

        }
        else
        {
            right.text = "";
        }
        if (PlayerPrefs.GetInt("SprintState", 0) > 1)
        {
            sprint.text = "Sprint: " + (KeyCode)PlayerPrefs.GetInt("Sprint", (int)KeyCode.A);

        }
        else
        {
            sprint.text = "";
        }
        if (PlayerPrefs.GetInt("JumpState", 0) > 1)
        {
            jump.text = "Jump: " + (KeyCode)PlayerPrefs.GetInt("Jump", (int)KeyCode.A);

        }
        else
        {
            jump.text = "";
        }


    }

    public void RebindLeft()
    {
        isRebindingLeft = true;
        isRebinding = true;
    }

    public void RebindRight()
    {
        isRebindingRight = true;
        isRebinding = true;
    }

    public void RebindSprint()
    {
        isRebindingSprint = true;
        isRebinding = true;
    }

    public void RebindJump()
    {
        isRebindingJump = true;
        isRebinding = true;
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
                        break;
                    }
                }
            }
        }
    }
}
