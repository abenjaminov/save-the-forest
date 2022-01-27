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

        private PlayerMovement _playerMovement;
        private Animator _animator;

        private void Awake()
        {
            _StateMachine = new StateMachine();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();

            _inputChannel.SubscribeAction(InputActionTypes.Move, OnMoveAction);
        }

        private void OnMoveAction(InputActionOptions options)
        {
            _moveState.MovementDirection = options.MovementDirection;
        }

        private void Start()
        {
            _idleState = new PlayerIdleState(_animator, _playerMovement);
            _moveState = new PlayerMoveState(_animator, _playerMovement);

            var shouldIdle = new Func<bool>(() => _moveState.MovementDirection == Vector2.zero);
            var shouldMove = new Func<bool>(() => _moveState.MovementDirection != Vector2.zero);
            
            _StateMachine.AddTransition(_idleState, shouldIdle);     
            _StateMachine.AddTransition(_moveState, shouldMove, _idleState);
            
            _StateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _StateMachine.Tick();
        }
    }
}