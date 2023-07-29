using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private Vector3 moveDirection;

    void Start()
    {

    }

    void Update()
    {
        Vector2 direction = joystick.direction;

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            transform.rotation = targetRotation;

            moveDirection = moveDirection * speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        if (anim.GetBool("Moving") != (direction != Vector2.zero))
        {
            anim.SetBool("Moving", direction != Vector2.zero);
        }

        anim.SetBool("Carry", GameManager.instance.collector.HasItem());
    }

}
