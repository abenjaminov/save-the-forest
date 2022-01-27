using System;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using ScriptableObjects.Channels;
using State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerStates : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputchannel;
        private StateMachine _StateMachine;

        private PlayerIdleState _idleState;

        private PlayerMovement _playerMovement;
        private Animator _animator;

        private Vector2 _movementDirection;
        
        private void Awake()
        {
            _StateMachine = new StateMachine();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();

            _inputchannel.SubscribeAction(InputActionTypes.Move, OnMoveAction);
        }

        private void OnMoveAction(InputActionOptions options)
        {
            _movementDirection = options.MovementDirection;
        }

        private void Start()
        {
            _idleState = new PlayerIdleState(_animator, _playerMovement);

            var shouldIdle = new Func<bool>(() => _movementDirection == Vector2.zero);
            
            _StateMachine.AddTransition(_idleState, shouldIdle);     
        }
    }
}