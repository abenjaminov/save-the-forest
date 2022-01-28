using System;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Objectives;
using UnityEngine;

namespace _Scripts.Objectives
{
    public class ObjectivesManager : MonoBehaviour
    {
        [SerializeField] private ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private ActionObjective FirstObjective;

        private void Awake()
        {
            _ObjectivesChannel.OnObjectiveCompleteEvent += OnObjectiveCompleteEvent;
        }

        private void Start()
        {
            FirstObjective.Activate();
        }

        private void OnObjectiveCompleteEvent(ActionObjective objective)
        {
            var nextObjective = objective.NextObjective;
            
            if(nextObjective != null)
            {
                nextObjective.Activate();
            }
        }
    }
}