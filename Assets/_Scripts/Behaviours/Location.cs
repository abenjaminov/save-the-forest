using System;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(BoxCollider))]
    public class Location : MonoBehaviour
    {
        [SerializeField] private PlayerChannel _PlayerChannel;
        [SerializeField] private ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private string _arriveAtLocationGuid;
        [SerializeField] private string _interactGuid;

        private bool _canInteract;

        private void Awake()
        {
            _PlayerChannel.OnPlayerInteractEvent += OnPlayerInteractEvent;
        }

        private void OnPlayerInteractEvent()
        {
            if (!_canInteract) return;
            
            _ObjectivesChannel.OnActionEvent(_interactGuid);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _canInteract = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _canInteract = true;
            _ObjectivesChannel.OnActionEvent(_arriveAtLocationGuid); 
        }
        
        
    }
}