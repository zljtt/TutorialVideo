using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public const float gravity = 9.18f;
    public float jumpHeight;
    public float airControl;
    private Vector3 moveDirection;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveHorizontal * moveSpeed + transform.forward * moveVertical * moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = move;
            if (Input.GetAxis("Jump") > 0)
            {
                moveDirection.y = Mathf.Sqrt(gravity * 2 * jumpHeight);
            }
            else
            {
                moveDirection.y = 0;

            }
        }
        else
        {
            move.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, move, Time.deltaTime * airControl);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
