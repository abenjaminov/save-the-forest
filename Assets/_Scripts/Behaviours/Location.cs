using System;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(BoxCollider))]
    public class Location : MonoBehaviour
    {
        [SerializeField] private ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private string LocationGuid;
        private void OnTriggerEnter(Collider other)
        {
            _ObjectivesChannel.OnEnterLocation(LocationGuid); 
        }
    }
}