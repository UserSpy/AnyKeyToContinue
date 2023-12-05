using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float verticalOffset = 0.15f;
    public Transform target;
    public float multi = 1.1f;
    public float springBack = 2;
    public float maxDistance = 5f;

    public float resetSpeed = 2f;
    private float maxDistanceInit;
    public float initialScale = 1f;
    public float scaleIncrease = 0.01f;
    private float scale;
    private Rigidbody2D rb;
    private Vector3 newVelocity;
    private float distancex;
    private float distancey;
    private float initialZPosition = -10f;
    private float initialCameraSize;
    // Start is called before the first frame update
    void Start()
    {
        rb = target.GetComponent<Rigidbody2D>();
        initialZPosition = transform.position.z;
        initialCameraSize = GetComponent<Camera>().orthographicSize;
        maxDistanceInit = maxDistance;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scale = initialScale + scaleIncrease * target.transform.childCount;
        GetComponent<Camera>().orthographicSize = initialCameraSize * scale;
        maxDistance = scale * maxDistanceInit;
        Vector3 currentPosition = transform.position;
        newVelocity = rb.velocity * Time.deltaTime;
        distancex = currentPosition.x - rb.position.x;
        distancey = currentPosition.y - rb.position.y;




        if (Mathf.Abs(distancex) < maxDistance)
        {
            currentPosition.x += newVelocity.x * multi;
        }
        else
        {
            currentPosition.x += newVelocity.x / multi;
        }

        if (((distancex < 0) && (newVelocity.x > 0)) || ((distancex > 0) && (newVelocity.x < 0)))
        {
            currentPosition.x += newVelocity.x * springBack;
        }


        if (((distancey < 0) && (newVelocity.y > 0)) || ((distancey > 0) && (newVelocity.y < 0)))
        {
            currentPosition.y += newVelocity.y * springBack;
        }

        if (Mathf.Abs(currentPosition.y - rb.position.y) < maxDistance)
        {
            currentPosition.y += newVelocity.y * multi;
        }
        else
        {
            currentPosition.y += newVelocity.y;
        }
        currentPosition.z = initialZPosition / multi;

        currentPosition.x = Mathf.Clamp(currentPosition.x, rb.position.x - maxDistance, rb.position.x + maxDistance);


        currentPosition.y = Mathf.Clamp(currentPosition.y, rb.position.y - maxDistance, rb.position.y + maxDistance);
        transform.position = currentPosition;

        if (rb.velocity.magnitude < 0.1f)
        {
            Vector2 resetPosition = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y), Time.deltaTime * resetSpeed);
            transform.position = new Vector3(resetPosition.x, resetPosition.y, initialZPosition);
        }
    }
}
