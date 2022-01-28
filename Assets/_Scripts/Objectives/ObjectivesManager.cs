using System;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Objectives;
using UnityEngine;

namespace _Scripts.Objectives
{
    public class ObjectivesManager : MonoBehaviour
    {
        [SerializeField] private ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private Objective FirstObjective;

        private void Awake()
        {
            FirstObjective.Activate();
            
            _ObjectivesChannel.OnObjectiveCompleteEvent += OnObjectiveCompleteEvent;
        }

        private void OnObjectiveCompleteEvent(Objective objective)
        {
            var nextObjective = objective.NextObjective;
            
            if(nextObjective != null)
            {
                nextObjective.Activate();
            }
        }
    }
}