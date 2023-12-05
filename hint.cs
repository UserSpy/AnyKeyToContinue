using UnityEngine;
using System.Collections;
using TMPro;

public class hint : MonoBehaviour
{
    public TextMeshPro chatText; 
    public float messageChangeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMessageAfterTime());
    }

    public void ShowMessage()
    {
        chatText.text = "Sprint is " + (KeyCode)PlayerPrefs.GetInt("Sprint");
    }

    private IEnumerator ChangeMessageAfterTime()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(messageChangeTime);

        // Change the message
        ShowMessage();
    }
}