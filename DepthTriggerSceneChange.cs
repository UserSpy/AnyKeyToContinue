using UnityEngine;
using UnityEngine.SceneManagement;

public class DepthTriggerSceneChange : MonoBehaviour
{
    public float triggerDepth = -10f; 
    public int sceneToLoad = 2;

    void Update()
    {
        if (transform.position.y <= triggerDepth)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}