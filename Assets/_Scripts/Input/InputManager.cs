using _Scripts.ScriptableObjects.Channels.Input;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;

        private void Awake()
        {
            PlayerInput playerInput = GetComponent<PlayerInput>();
            var attack1 = playerInput.actions.FindAction("Attack1");
            attack1.started += Attack1_started;
            attack1.canceled += Attack1_canceled;

            var attack2 = playerInput.actions.FindAction("Attack2");
            attack2.started += Attack2_started;
            attack2.canceled += Attack2_canceled;
        }

        private void Attack1_canceled(InputAction.CallbackContext obj)
        {
            var inputOptions = new InputActionOptions()
            {
                isAttack1Pressed = false
            };
            _inputChannel.OnAction(InputActionTypes.Attack1, inputOptions);
        }

        private void Attack1_started(InputAction.CallbackContext obj)
        {
            var inputOptions = new InputActionOptions()
            {
                isAttack1Pressed = true
            };
            _inputChannel.OnAction(InputActionTypes.Attack1, inputOptions);
        }

        private void Attack2_canceled(InputAction.CallbackContext obj)
        {
            var inputOptions = new InputActionOptions()
            {
                isAttack2Pressed = false
            };
            _inputChannel.OnAction(InputActionTypes.Attack2, inputOptions);
        }

        private void Attack2_started(InputAction.CallbackContext obj)
        {
            var inputOptions = new InputActionOptions()
            {
                isAttack2Pressed = true
            };
            _inputChannel.OnAction(InputActionTypes.Attack2, inputOptions);
        }

        void OnMove(InputValue inputValue)
        {
            var inputOptions = new InputActionOptions()
            {
                MovementDirection = inputValue.Get<Vector2>()
            };

            _inputChannel.OnAction(InputActionTypes.Move, inputOptions);
        }

        void OnJump(InputValue inputValue)
        {
            _inputChannel.OnAction(InputActionTypes.Jump, null);
        }
    }
}