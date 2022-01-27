using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        private float actualMoveSpeed;
        Vector2 movementDirection;
        CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            actualMoveSpeed = 0;
        }
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var positionDelta = new Vector3(movementDirection.x, 0, movementDirection.y) * actualMoveSpeed * Time.deltaTime;
            var targetAngle = Mathf.Atan2(positionDelta.x, positionDelta.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            characterController.Move(positionDelta);
        }
        
        public void Idle()
        {
            actualMoveSpeed = 0;
        }

        public void Move(Vector2 direction)
        {
            actualMoveSpeed = moveSpeed;
            movementDirection = direction;
        }
    }
}
