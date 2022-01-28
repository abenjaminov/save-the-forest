using System;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Channels.Input;
using _Scripts.ScriptableObjects.Channels.Input.Models;
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
        
        private BearHandAttackState _bearHandAttackState;
        private BearFrontAttackState _bearFrontState;
        
        private PlayerMovement _playerMovement;
        private PlayerVisuals _PlayerVisuals;
        private AnimatorController _animator;
        private bool _isJumpPressed;
        private bool _isAttack1Pressed;
        private bool _isAttack2Pressed;

        private void Awake()
        {
            _StateMachine = new StateMachine();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<AnimatorController>();
            _PlayerVisuals = GetComponent<PlayerVisuals>();

            _playerChannel.PlayerChangeShapeEvent += PlayerChangeShapeEvent;
            _inputChannel.SubscribeAction(InputActionTypes.Move, OnMoveAction);
            _inputChannel.SubscribeAction(InputActionTypes.Jump, OnJumpAction);
            _inputChannel.SubscribeAction(InputActionTypes.Attack1, OnBearHandAttackAction);
            _inputChannel.SubscribeAction(InputActionTypes.Attack2, OnBearFrontAttackAction);
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

        private void OnBearHandAttackAction(InputActionOptions options)
        {
            _isAttack1Pressed = options.isAttack1Pressed;
        }

        private void OnBearFrontAttackAction(InputActionOptions options)
        {
            _isAttack2Pressed = options.isAttack2Pressed;
        }

        private void Start()
        {
            _idleState = new PlayerIdleState(_animator, _playerMovement);
            _moveState = new PlayerMoveState(_animator, _playerMovement);
            _jumpState = new PlayerJumpState(_animator, _playerMovement);
            _changeShapeState = new PlayerChangeShapeState(_animator, _PlayerVisuals);
            _bearHandAttackState = new BearHandAttackState(_animator);
            _bearFrontState = new BearFrontAttackState(_animator);

            var shouldIdle = new Func<bool>(() => _moveState.MovementDirection == Vector2.zero);
            var shouldMove = new Func<bool>(() => _moveState.MovementDirection != Vector2.zero);
            var shouldJump = new Func<bool>(() => _playerMovement.IsGrounded() && _isJumpPressed);
            var shouldHandAttack = new Func<bool>(() => _PlayerVisuals.CurrentShape == PlayerShape.Bear &&
                                                        _isAttack1Pressed);
            var shouldFrontAttack = new Func<bool>(() => _PlayerVisuals.CurrentShape == PlayerShape.Bear &&
                                                         _isAttack2Pressed);

            _StateMachine.AddTransition(_jumpState, shouldJump, _idleState, () =>
            {
                _isJumpPressed = false;
            },"From Idle To Jumping State");
            _StateMachine.AddTransition(_jumpState, shouldJump, _moveState, () =>
            {
                _isJumpPressed = false;
            }, "From Move To Jumping State");

            _StateMachine.AddTransition(_bearHandAttackState, shouldHandAttack, _idleState, transitionName: "From Idle To Bear Hand Attack State");
            _StateMachine.AddTransition(_bearHandAttackState, shouldHandAttack, _moveState, transitionName: "From Move To Bear Hand Attack State");
            _StateMachine.AddTransition(_bearHandAttackState, shouldHandAttack, _jumpState, transitionName: "From Jump To Bear Hand Attack State");

            _StateMachine.AddTransition(_bearFrontState, shouldFrontAttack, _idleState, transitionName: "From Idle To Bear Front Attack State");
            _StateMachine.AddTransition(_bearFrontState, shouldFrontAttack, _moveState, transitionName: "From Move To Bear Front Attack State");
            _StateMachine.AddTransition(_bearFrontState, shouldFrontAttack, _jumpState, transitionName: "From Jump To Bear Front Attack State");

            _StateMachine.AddTransition(_moveState, shouldMove, _idleState, transitionName: "From Idle To Moving State");
            _StateMachine.AddTransition(_moveState, shouldMove, _jumpState, transitionName: "From Jump To Moving State");
            _StateMachine.AddTransition(_moveState, shouldMove, _changeShapeState, transitionName: "From Jump To Moving State");
            _StateMachine.AddTransition(_moveState, shouldMove, _bearHandAttackState, transitionName: "From Attack1 To Moving State");
            _StateMachine.AddTransition(_moveState, shouldMove, _bearFrontState, transitionName: "From Attack2 To Moving State");

            _StateMachine.AddTransition(_idleState, shouldIdle, _moveState, transitionName: "From Move To Idle State");
            _StateMachine.AddTransition(_idleState, shouldIdle, _jumpState, transitionName: "From Jump To Idle State");
            _StateMachine.AddTransition(_idleState, shouldIdle, _changeShapeState, transitionName: "From Jump To Idle State");
            _StateMachine.AddTransition(_idleState, shouldIdle, _bearHandAttackState, transitionName: "From Attack1 To Idle State");
            _StateMachine.AddTransition(_idleState, shouldIdle, _bearFrontState, transitionName: "From Attack2 To Idle State");

            _StateMachine.SetState(_idleState);
            
            var shouldShapeShift = new Func<bool>(() => _PlayerVisuals.CurrentShape != _changeShapeState.Shape);

            _StateMachine.AddTransition(_changeShapeState, shouldShapeShift, _idleState);
            _StateMachine.AddTransition(_changeShapeState, shouldShapeShift, _moveState);
        }

        private void Update()
        {
            _StateMachine.Tick();
        }
    }
}