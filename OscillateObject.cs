using UnityEngine;

public class OscillateObject : MonoBehaviour
{
    public float speed = 5f;        // Initial speed of oscillation.
    public float maxDistance = 2f;  // Maximum distance of oscillation from the starting point.
    public float acceleration = 0.1f; // Rate of speed increase per second.
    public float expansion = 0.1f;    // Rate of max distance increase per second.

    private float originalX;
    private float nextX;           // Next position along the x-axis to move to.
    private float currentSpeed;    // Current speed of the object.

    private void Start()
    {
        originalX = transform.position.x;
        currentSpeed = speed;
        nextX = originalX;
    }

    private void FixedUpdate()
    {
        // Update the current speed and max distance
        currentSpeed += acceleration * Time.fixedDeltaTime;
        maxDistance += expansion * Time.fixedDeltaTime;

        // Calculate target position within the allowed range
        float maxOffset = maxDistance / 2f;
        float pingPong = Mathf.PingPong(Time.fixedTime * currentSpeed, maxDistance);
        nextX = originalX + pingPong - maxOffset;

        // Move towards the target position
        transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
    }
}
