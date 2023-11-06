using UnityEngine;
using System.Collections;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public TextMeshPro chatText; 
    public float messageChangeTime = 10f;
    private string firstMessage = "Hello there! Jump over here!";
    private string secondMessage = "Do you not know how to move?";
    private string thirdMessage = "Maybe try every key?";

    // Start is called before the first frame update
    void Start()
    {
        ShowMessage(firstMessage);
        StartCoroutine(ChangeMessageAfterTime());
    }

    public void ShowMessage(string message)
    {
        chatText.text = message;
    }

    private IEnumerator ChangeMessageAfterTime()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(messageChangeTime);

        // Change the message
        ShowMessage(secondMessage);

        yield return new WaitForSeconds(messageChangeTime);

        ShowMessage(thirdMessage);
    }
}