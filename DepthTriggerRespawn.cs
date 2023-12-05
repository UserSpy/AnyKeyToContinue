using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTriggerRespawn : MonoBehaviour
{
    public float triggerDepth = -10f; 

    void Update()
    {
        if (transform.position.y <= triggerDepth)
        {
            transform.position = new Vector3(0, 4, transform.position.z);
        }
    }
}
