using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    [CreateAssetMenu(fileName = "Go to location", menuName = "Objectives/Go to location", order = 0)]
    public class GoToLocationObjective : Objective
    {
        public string LocationGuid;

        private void OnDisable()
        {
            _ObjectivesChannel.OnEnterLocationEvent -= OnEnterLocationEvent;
        }

        private void OnEnable()
        {
            _ObjectivesChannel.OnEnterLocationEvent += OnEnterLocationEvent;
        }

        private void OnEnterLocationEvent(string locatioGuid)
        {
            if (locatioGuid != this.LocationGuid) return;
        }
    }
}