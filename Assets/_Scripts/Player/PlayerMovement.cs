using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpSpeed = 10f;
        [SerializeField] float gravity = 20f;
        private float horizontalSpeed;
        private float verticalSpeed;
        CharacterController characterController;

        Vector3 playerMovment;

        

        float distanceGround;
        bool isGrounded = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            horizontalSpeed = 0;

            distanceGround = GetComponent<Collider>().bounds.extents.y;
        }
        
        private void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f))
            {
                isGrounded = false;
            }
            else
            {
                isGrounded = true;
            }
        }

        private void Move()
        {
            playerMovment.Normalize();

            if (!isGrounded && verticalSpeed >= -gravity)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }

            var moveDirection = new Vector3(playerMovment.x * horizontalSpeed, 
                                            verticalSpeed, 
                                            playerMovment.z * horizontalSpeed);
            var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            characterController.Move(moveDirection  * Time.deltaTime);
        }
        
        public void Idle()
        {
            horizontalSpeed = 0;
        }

        public void Move(Vector2 direction)
        {
            horizontalSpeed = moveSpeed;
            playerMovment.x = direction.x;
            playerMovment.z = direction.y;
        }

        public void Jump()
        {
            verticalSpeed = jumpSpeed;
        }

        public bool IsGrounded()
        {
            return isGrounded;
        }
    }
}
