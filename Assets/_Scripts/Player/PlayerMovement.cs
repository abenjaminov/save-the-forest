using _Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Health _Health;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpSpeed = 10f;
        [SerializeField] float gravity = 20f;
        private float horizontalSpeed;
        private float verticalSpeed;
        private CharacterController characterController;

        Vector3 playerMovment;
        bool isGrounded = false;

        private void Awake()
        {
            _Health = GetComponent<Health>();
            horizontalSpeed = 0;
            characterController = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
            Move();

            if (transform.position.y < -50)
            {
                _Health.Die();
            }
        }

        public void RefreshCharatercontroller(PlayershapeInfo info)
        {
            var shapeCharacterController = GetComponent<CharacterController>();

            shapeCharacterController.height = info.Height;
            shapeCharacterController.radius = info.Radius;
            shapeCharacterController.center = info.Center;
            moveSpeed = info.moveSpeed;
            jumpSpeed = info.jumpSpeed;
        }

        private void FixedUpdate()
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 1.6f))
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

            if (verticalSpeed >= -gravity)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }

            var moveDirection = new Vector3(playerMovment.x * horizontalSpeed, 
                                            0f, 
                                            playerMovment.z * horizontalSpeed);

            if (moveDirection.magnitude > 0.1f && horizontalSpeed > 0)
            {
                var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }

            moveDirection.y = verticalSpeed;
            characterController.Move(moveDirection * Time.deltaTime);
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


