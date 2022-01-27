using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpSpeed = 10f;
        private float actualMoveSpeed;
        CharacterController characterController;

        Vector3 playerMovment;

        float gravity = 9.81f;
        float groundedGravity = 0.05f;

        float distanceGround;
        bool isGrounded = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            actualMoveSpeed = 0;

            distanceGround = GetComponent<Collider>().bounds.extents.y;
        }
        
        private void Update()
        {
            HandleGravity();
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
            var moveDirection = new Vector3(playerMovment.x, playerMovment.y, playerMovment.z);

            var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            characterController.Move(moveDirection * actualMoveSpeed * Time.deltaTime);
        }

        private void HandleGravity()
        {
            if (isGrounded)
            {
                playerMovment.y -= groundedGravity;
            }
            else
            {
                playerMovment.y -= gravity;
            }
        }
        
        public void Idle()
        {
            actualMoveSpeed = 0;
        }

        public void Move(Vector2 direction)
        {
            actualMoveSpeed = moveSpeed;
            playerMovment.x = direction.x;
            playerMovment.z = direction.y;
        }

        public void Jump()
        {
            playerMovment.y = jumpSpeed;
        }

        public bool IsGrounded()
        {
            return isGrounded;
        }
    }
}
