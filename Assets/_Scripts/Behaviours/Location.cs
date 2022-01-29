using System;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(BoxCollider))]
    public class Location : MonoBehaviour
    {
        [SerializeField] private UIChannel _UIChannel;
        [SerializeField] private PlayerChannel _PlayerChannel;
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private GameAction _arriveAtLocationAction;
        [SerializeField] private GameAction _interactAction;

        private bool _canInteract;

        private void OnPlayerInteractEvent()
        {
            _GameChannel.OnAction(_interactAction);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _PlayerChannel.OnPlayerInteractEvent -= OnPlayerInteractEvent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _PlayerChannel.OnPlayerInteractEvent += OnPlayerInteractEvent;
            
            if(_arriveAtLocationAction != null)
                _GameChannel.OnAction(_arriveAtLocationAction);
        }

        private void OnDisable()
        {
            _PlayerChannel.OnPlayerInteractEvent -= OnPlayerInteractEvent;
        }
    }
}