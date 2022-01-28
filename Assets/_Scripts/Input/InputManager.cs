using _Scripts.ScriptableObjects.Channels.Input.Models;
using ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;

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

        void OnAttack1(InputValue inputValue)
        {
            _inputChannel.OnAction(InputActionTypes.Attack1, null);
        }
    }
}