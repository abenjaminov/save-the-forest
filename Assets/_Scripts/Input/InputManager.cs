using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Channels.Input;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private InputChannel _inputChannel;

        private PlayerInput _PlayerInput;

        private void OnDestroy()
        {
            _GameChannel.OnShowStoryEvent -= OnShowStoryEvent;
            _GameChannel.OnStoryToldEvent -= OnStoryToldEvent;
        }

        private void Awake()
        {
            _GameChannel.OnShowStoryEvent += OnShowStoryEvent;
            _GameChannel.OnStoryToldEvent += OnStoryToldEvent;
            
            _PlayerInput = GetComponent<PlayerInput>();
            var attack1 = _PlayerInput.actions.FindAction("Attack1");
            attack1.started += Attack1_started;
            attack1.canceled += Attack1_canceled;
            
            var attack2 = _PlayerInput.actions.FindAction("Attack2");
            attack2.started += Attack2_started;
            attack2.canceled += Attack2_canceled;
        }

        private void OnStoryToldEvent(StoryItem arg0)
        {
            _PlayerInput.actions.Enable();
            _PlayerInput.actions.FindAction("SkipStory").Disable();
        }

        private void OnShowStoryEvent(StoryItem arg0)
        {
            _PlayerInput.actions.Disable();
            _PlayerInput.actions.FindAction("SkipStory").Enable();
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

        void OnSkipStory(InputValue inputValue)
        {
            _inputChannel.OnAction(InputActionTypes.SkipStory, null);
		}
		
        void OnShiftToHuman(InputValue inputValue)
        {
            var inputOptions = new InputActionOptions()
            {
                PlayerShape = Player.PlayerShape.Human
            };
            _inputChannel.OnAction(InputActionTypes.ShiftToHuman, inputOptions);
        }

        void OnShiftToBear(InputValue inputValue)
        {
            var inputOptions = new InputActionOptions()
            {
                PlayerShape = Player.PlayerShape.Bear
            };
            _inputChannel.OnAction(InputActionTypes.ShiftToBear, inputOptions);
        }

        void OnShiftToRabbit(InputValue inputValue)
        {
            var inputOptions = new InputActionOptions()
            {
                PlayerShape = Player.PlayerShape.Rabbit
            };
            _inputChannel.OnAction(InputActionTypes.ShiftToRabbit, inputOptions);
        }
    }
}