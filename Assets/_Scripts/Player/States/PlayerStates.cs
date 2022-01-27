using System;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using ScriptableObjects.Channels;
using State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerStates : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private PlayerChannel _playerChannel;
        private StateMachine _StateMachine;

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        private PlayerJumpState _jumpState;
        private PlayerChangeShapeState _changeShapeState;
        private PlayerMovement _playerMovement;
        private PlayerVisuals _PlayerVisuals;
        private Animator _animator;
        private bool _isGrounded;
        private bool _isJumpPressed;

        private void Awake()
        {
            _StateMachine = new StateMachine(true);
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();
            _PlayerVisuals = GetComponent<PlayerVisuals>();

            _playerChannel.PlayerChangeShapeEvent += PlayerChangeShapeEvent;
            _inputChannel.SubscribeAction(InputActionTypes.Move, OnMoveAction);
            _inputChannel.SubscribeAction(InputActionTypes.Jump, OnJumpAction);
        }

        private void PlayerChangeShapeEvent(PlayerShape shape)
        {
            _changeShapeState.Shape = shape;
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
            _changeShapeState = new PlayerChangeShapeState(_animator, _PlayerVisuals);

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
            
            var shouldShapeShift = new Func<bool>(() => /*IsGrounded &&*/_PlayerVisuals.CurrentShape != _changeShapeState.Shape);
            
            _StateMachine.AddTransition(_changeShapeState, shouldShapeShift, _idleState);
            _StateMachine.AddTransition(_changeShapeState, shouldShapeShift, _moveState);
        }

        private void Update()
        {
            _StateMachine.Tick();
        }
    }
}