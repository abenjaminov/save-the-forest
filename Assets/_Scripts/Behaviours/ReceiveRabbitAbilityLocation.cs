using System;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Behaviours
{
    public class ReceiveRabbitAbilityLocation : MonoBehaviour
    {
        [SerializeField] private GameAction AriveAtReceiveRabbitAbilityLocationAction;
        [SerializeField] private GameChannel _GameChannel;

        private void Awake()
        {
            _GameChannel.OnActionEvent += OnActionEvent;
        }

        private void OnActionEvent(GameAction arg0)
        {
            if (arg0.Guid != AriveAtReceiveRabbitAbilityLocationAction.Guid) return;
            
            _GameChannel.OnReceiveRabbitAbility();
        }
    }
}