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
        
        private void Awake()
        {
            _StateMachine = new StateMachine();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();

            _inputchannel.SubscribeAction(InputActionTypes.Move, OnInputAction);
        }

        private void OnInputAction(InputActionOptions options)
        {
            
        }

        private void Start()
        {
            _idleState = new PlayerIdleState(_animator, _playerMovement);
        }
    }
}