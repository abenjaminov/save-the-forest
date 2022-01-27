using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        Vector2 playerInput;
        CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 positionDelta = new Vector3(playerInput.x, 0, playerInput.y) * moveSpeed * Time.deltaTime;
            characterController.Move(positionDelta);
        }

        void OnMove(InputValue inputValue)
        {
            playerInput = inputValue.Get<Vector2>();
            
        }
    }
}
