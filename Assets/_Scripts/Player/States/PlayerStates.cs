using System;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using ScriptableObjects.Channels;
using State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerStates : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        private StateMachine _StateMachine;

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        private PlayerJumpState _jumpState;

        private PlayerMovement _playerMovement;
        private Animator _animator;
        private bool _isGrounded;
        private bool _isJumpPressed;

        private void Awake()
        {
            _StateMachine = new StateMachine(true);
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();

            _inputChannel.SubscribeAction(InputActionTypes.Move, OnMoveAction);
            _inputChannel.SubscribeAction(InputActionTypes.Jump, OnJumpAction);
        }

        private void OnMoveAction(InputActionOptions options)
        {
            _moveState.MovementDirection = options.MovementDirection;
        }

        private void OnJumpAction(InputActionOptions options)
        {
            _isJumpPressed = true;
        }

        private void Start()
        {
            _idleState = new PlayerIdleState(_animator, _playerMovement);
            _moveState = new PlayerMoveState(_animator, _playerMovement);
            _jumpState = new PlayerJumpState(_animator, _playerMovement);

            var shouldIdle = new Func<bool>(() => _moveState.MovementDirection == Vector2.zero);
            var shouldMove = new Func<bool>(() => _moveState.MovementDirection != Vector2.zero);
            var shouldJump = new Func<bool>(() => _playerMovement.IsGrounded() && _isJumpPressed);
            
            _StateMachine.AddTransition(_jumpState, shouldJump, _idleState, () =>
            {
                _isJumpPressed = false;
            },"To Jumping State");
            
            _StateMachine.AddTransition(_moveState, shouldMove, _idleState, transitionName: "To Moving State");
            _StateMachine.AddTransition(_moveState, shouldMove, _jumpState, transitionName: "To Moving State");

            _StateMachine.AddTransition(_idleState, shouldIdle, _moveState, transitionName: "To Idle State");
            _StateMachine.AddTransition(_idleState, shouldIdle, _jumpState, transitionName: "To Idle State");

            _StateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _StateMachine.Tick();
        }
    }
}