using UnityEngine;

public class DefenderAI : MonoBehaviour
{
    public Transform player;               // Reference to the player (attacker)
    public float distanceToMaintain = 2.0f; // Distance the defender should maintain from the player
    public float speed = 2.0f;
    private CharacterController controller; // CharacterController component

    private Vector3 initialPosition;       // Initial position of the defender

    void Start()
    {
        // Get the CharacterController component on the defender
        controller = GetComponent<CharacterController>();
        initialPosition = transform.position; // Store the initial position for rotation calculations
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Keep the movement and rotation on the horizontal plane

        // Get the distance from the defender to the player
        float distance = directionToPlayer.magnitude;

        // If the defender is too far from the player, move towards the player
        if (distance > distanceToMaintain)
        {
            Vector3 moveDirection = directionToPlayer.normalized * (distance - distanceToMaintain);
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        // Face the player
        float angle = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -angle + 90, 0); // Rotate the defender to face the player
    }

    void LateUpdate()
    {
        // Set the Y position to 0 to keep the defender grounded
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        transform.position = currentPosition;
    }
}
