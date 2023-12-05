using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 10.0f;
    public float sprintMulti = 1.5f;
    public float jumpSpeed = 10.0f;
    private float finalSpeed = 10.0f;
    private bool isGrounded;
    public KeyCode moveLeft = KeyCode.A;
    public int moveLeftState = 0;
    public KeyCode moveRight = KeyCode.D;
    public int moveRightState = 0;
    public KeyCode sprint = KeyCode.LeftShift;
    public int sprintState = 0;
    public KeyCode jump = KeyCode.Space;
    public int jumpState = 0;

    private KeyCode[][] keyArray = {
    new KeyCode[] {
        KeyCode.BackQuote, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace,
    },
    new KeyCode[] {
        KeyCode.Tab, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.Backslash,
    },
    new KeyCode[] {
        KeyCode.None, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Return,
    },
    new KeyCode[] {
        KeyCode.LeftShift, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.RightShift, KeyCode.UpArrow,
    },
    new KeyCode[] {
        KeyCode.LeftControl, KeyCode.None, KeyCode.LeftAlt, KeyCode.Space, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.RightAlt, KeyCode.RightControl, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow,
    },
    new KeyCode[] {
        KeyCode.Mouse0, KeyCode.Mouse1
    }
};

    private KeyCode[] ctrlRestricted = {
    KeyCode.A,
    KeyCode.C,
    KeyCode.F,
    KeyCode.N,
    KeyCode.O,
    KeyCode.P,
    KeyCode.S,
    KeyCode.V,
    KeyCode.X,
    KeyCode.Y,
    KeyCode.Z,
    KeyCode.LeftAlt,
    KeyCode.RightAlt
    };

    private KeyCode[] altRestricted = {
    KeyCode.Tab
    };

    private List<int> touchedRow = new List<int>();
    private List<int> touchedCol = new List<int>();
    private List<KeyCode> usedSingles = new List<KeyCode>();
    // Start is called before the first frame update

    private int currentSceneIndex = 0; 
    public int firstSceneIndex = 0; 

    void SaveControls()
    {
        PlayerPrefs.SetInt("MoveLeft", (int)moveLeft);
        PlayerPrefs.SetInt("MoveRight", (int)moveRight);
        PlayerPrefs.SetInt("Sprint", (int)sprint);
        PlayerPrefs.SetInt("Jump", (int)jump);
        PlayerPrefs.SetInt("MoveLeftState", moveLeftState);
        PlayerPrefs.SetInt("MoveRightState", moveRightState);
        PlayerPrefs.SetInt("SprintState", sprintState);
        PlayerPrefs.SetInt("JumpState", jumpState);
        PlayerPrefs.Save();
    }

    public void LoadControls()
    {
        moveLeft = (KeyCode)PlayerPrefs.GetInt("MoveLeft", (int)KeyCode.A);
        moveLeftState = PlayerPrefs.GetInt("MoveLeftState", 0);
        moveRight = (KeyCode)PlayerPrefs.GetInt("MoveRight", (int)KeyCode.D);
        moveRightState = PlayerPrefs.GetInt("MoveRightState", 0);
        sprint = (KeyCode)PlayerPrefs.GetInt("Sprint", (int)KeyCode.LeftShift);
        sprintState = PlayerPrefs.GetInt("SprintState", 0);
        jump = (KeyCode)PlayerPrefs.GetInt("Jump", (int)KeyCode.Space);
        jumpState = PlayerPrefs.GetInt("JumpState", 0);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.HasKey("MoveLeft") && currentSceneIndex != firstSceneIndex)
        {
            LoadControls();
        }
        else
        {
            // If not, generate random keycodes and save them
            moveLeft = GetNewRandomKey();
            moveRight = GetNewRandomKey();
            sprint = GetRandomKeyAvoidJamming();
            jump = GetRandomKeyAvoidJamming();
            SaveControls();
        }
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", sceneID);
        moveLeftState = PlayerPrefs.GetInt("MoveLeftState", 0);
        moveRightState = PlayerPrefs.GetInt("MoveRightState", 0);
        jumpState = PlayerPrefs.GetInt("JumpState", 0);
        sprintState = PlayerPrefs.GetInt("SprintState", 0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(sprint))
        {
            finalSpeed = speed * sprintMulti;
            if (PlayerPrefs.GetInt("SprintState", 0) == 0)
            {
                sprintState = 1;
                SaveControls();
            }
        }
        else
        {
            finalSpeed = speed;
        }

        if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight))
        {
            rb.velocity = new Vector2(-finalSpeed, rb.velocity.y);
            if (PlayerPrefs.GetInt("MoveLeftState", 0) == 0)
            {
                moveLeftState = 1;
                SaveControls();
            }
        }
        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft))
        {
            rb.velocity = new Vector2(finalSpeed, rb.velocity.y);
            if (PlayerPrefs.GetInt("MoveRightState", 0) == 0)
            {
                moveRightState = 1;
                SaveControls();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (isGrounded && Input.GetKey(jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if (PlayerPrefs.GetInt("JumpState", 0) == 0)
            {
                jumpState = 1;
                SaveControls();
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    KeyCode GetNewRandomKey()
    {
        int i;
        int j;
        KeyCode newCode;
        do
        {
            i = UnityEngine.Random.Range(0, keyArray.GetLength(0));
            j = UnityEngine.Random.Range(0, keyArray[i].Length);
            newCode = keyArray[i][j];

        } while (usedSingles.Contains(newCode) || newCode == KeyCode.None);
        if (i != keyArray.GetLength(0) - 1)
        {
            touchedRow.Add(i);
            touchedCol.Add(j);
        }
        usedSingles.Add(newCode);
        return newCode;
    }
    KeyCode GetRandomKeyAvoidJamming()
    {
        int i;
        int j;
        KeyCode newCode;
        do
        {
            do
            {
                i = UnityEngine.Random.Range(0, keyArray.GetLength(0));
            } while (touchedRow.Contains(i));
            do
            {
                j = UnityEngine.Random.Range(0, keyArray[i].Length);
            } while (touchedCol.Contains(j));

            newCode = keyArray[i][j];

        } while (usedSingles.Contains(newCode) || newCode == KeyCode.None);
        if (i != keyArray.GetLength(0) - 1)
        {
            touchedRow.Add(i);
            touchedCol.Add(j);
        }
        usedSingles.Add(newCode);
        return newCode;
    }
}
