using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Objectives;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public class ActiveOnObjective : MonoBehaviour
    {
        [SerializeField] private ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private ActionObjective _Objective;
        [SerializeField] private List<MonoBehaviour> _componentsToActivate;
        [SerializeField] private List<GameObject> _gameObjectsToActivate;

        private void OnDestroy()
        {
            _ObjectivesChannel.OnObjectiveActiveEvent -= OnObjectiveActiveEvent;
            _ObjectivesChannel.OnObjectiveCompleteEvent -= OnObjectiveCompleteEvent;
        }

        private void Awake()
        {
            _ObjectivesChannel.OnObjectiveActiveEvent += OnObjectiveActiveEvent;
            _ObjectivesChannel.OnObjectiveCompleteEvent += OnObjectiveCompleteEvent;
            
            ToggleActive(_Objective.State == ObjectiveState.Active);
        }

        void ToggleActive(bool isActive)
        {
            foreach (var comp in _componentsToActivate)
            {
                comp.enabled = isActive;
            }
            
            foreach (var comp in _gameObjectsToActivate)
            {
                comp.SetActive(isActive);
            }
        }

        private void OnObjectiveCompleteEvent(ActionObjective arg0)
        {
            if (arg0.GUID != _Objective.GUID) return;
            
            ToggleActive(false);
        }

        private void OnObjectiveActiveEvent(ActionObjective arg0)
        {
            if (arg0.GUID != _Objective.GUID) return;
            
            ToggleActive(true);
        }
    }
}