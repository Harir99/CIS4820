using UnityEngine;

public class GoalieAI : MonoBehaviour
{
    public Transform player;
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
        transform.rotation = Quaternion.Euler(0, -angle + 90, 0); 
    }
}
