using System;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public class ReceiveBearAbilityLocation : MonoBehaviour
    {
        [SerializeField] private GameAction AriveAtReceiveBearAbilityLocationAction;
        [SerializeField] private GameChannel _GameChannel;

        private void Awake()
        {
            _GameChannel.OnActionEvent += OnActionEvent;
        }

        private void OnActionEvent(GameAction arg0)
        {
            if (arg0.Guid != AriveAtReceiveBearAbilityLocationAction.Guid) return;
            
            _GameChannel.OnReceiveBearAbility();
        }
    }
}