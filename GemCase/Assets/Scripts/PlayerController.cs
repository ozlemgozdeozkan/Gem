using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private FloatingJoystick floatingJoystick;


    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float turnSpeed;


    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        JoystickMovement();
    }
    private void JoystickMovement()
    {
        float horizontal = floatingJoystick.Horizontal;
        float vertical = floatingJoystick.Vertical;

        Vector3 addedPos = new(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);

        characterController.Move(addedPos);

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            _animator.SetBool("isRunning", true);
        }
        else
            _animator.SetBool("isRunning", false);
    }
}