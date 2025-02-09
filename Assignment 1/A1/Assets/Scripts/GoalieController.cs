using UnityEngine;

public class GoalieAI : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2.0f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 directionToPlayer = player.position - initialPosition;
        directionToPlayer.y = 0;

        float angle = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
    }
}
