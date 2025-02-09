using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class ExampleClass : MonoBehaviour
{
    public float speed = 100.0F;
    public float rotateSpeed = 3.0F;
    private CharacterController controller;

    private void Start()
    {
      controller = GetComponent<CharacterController>();   
    }

    void Update()
    {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
        Vector3 currentPosition = transform.position;
        currentPosition.y = 40;
        transform.position = currentPosition;
    }
}