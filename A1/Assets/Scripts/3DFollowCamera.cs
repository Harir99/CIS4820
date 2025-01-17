using UnityEngine;

public class FollowCamera3D : MonoBehaviour
{
    public Transform player;          // Reference to the player's Transform
    public float distance = 3f;       // Distance the camera will maintain from the player
    public float smoothTime = 0.25f;  // Time for smoothing the camera's movement
    private Vector3 currentVelocity; // The current velocity of the camera (used by SmoothDamp)

    void LateUpdate()
    {
        // Calculate the target position
        Vector3 target = player.position + (transform.position - player.position).normalized * distance;

        // Prevent the camera from going below the player
        if (target.y < player.position.y)
        {
            target.y = player.position.y;
        }

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);

        // Make the camera face the player
        transform.LookAt(player);
    }
}
