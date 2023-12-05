using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOnHit : MonoBehaviour
{

    public SceneTransition sceneTransition; 
    public int sceneID;

    public GameObject player;
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject == player)
    {
        sceneTransition.ChangeScene(sceneID);
    }
}
}

