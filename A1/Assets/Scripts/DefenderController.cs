using UnityEngine;

public class DefenderAI : MonoBehaviour
{
    public Transform player;               // Reference to the player (attacker)
    public float distanceToMaintain = 2.0f; // Distance the defender should maintain from the player
    public float speed = 2.0f;
    private CharacterController controller; // CharacterController component

    void Start()
    {
        // Get the CharacterController component on the defender
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Keep the movement and rotation on the horizontal plane

        // Get the distance from the defender to the player
        float distance = directionToPlayer.magnitude;

        // If the defender is too far from the player, move towards the player (negative direction)
        if (distance > distanceToMaintain)
        {
            float moveDistance = distance - distanceToMaintain;
            Vector3 moveDirection = directionToPlayer.normalized * moveDistance;
            controller.Move(moveDirection * speed * Time.deltaTime);  // Move towards the player (negative direction)
        }
        // If the defender is too close to the player, move away to maintain the distance (positive direction)
        else if (distance < distanceToMaintain)
        {
            float moveDistance = distanceToMaintain - distance;
            Vector3 moveDirection = directionToPlayer.normalized * moveDistance;
            controller.Move(moveDirection * speed * Time.deltaTime);  // Move away from the player (positive direction)
        }

        // Face the player
        float angle = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        // Keep the defender on the ground by setting y to 0
        Vector3 currentPosition = transform.position;
        currentPosition.y = 3;
        transform.position = currentPosition;
    }
}
