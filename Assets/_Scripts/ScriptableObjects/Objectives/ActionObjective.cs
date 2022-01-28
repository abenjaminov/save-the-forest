using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    public enum ObjectiveState
    {
        Pending,
        Active,
        Complete
    }
    
    [CreateAssetMenu(fileName = "Action Objective", menuName = "Objectives/Action", order = 0)]
    public class ActionObjective : ScriptableObject
    {
        [SerializeField] protected ObjectivesChannel _ObjectivesChannel;
        public List<ActionInfo> ActionInfos;
        public string GUID;
        
        public string Description;
        public ActionObjective NextObjective;
        protected ObjectiveState State;

        
        private void OnDisable()
        {
            _ObjectivesChannel.OnActionEvent -= OnActionEvent;
        }

        private void OnEnable()
        {
            _ObjectivesChannel.OnActionEvent += OnActionEvent;

            foreach (var actionInfo in ActionInfos)
            {
                actionInfo.Happened = false;
            }
        }

        public void Activate()
        {
            State = ObjectiveState.Active;
            _ObjectivesChannel.OnObjectiveActive(this);
        }
        
        protected void Complete()
        {
            State = ObjectiveState.Complete;
            
            _ObjectivesChannel.OnObjectiveComplete(this);
        }
        
        private void OnActionEvent(GameAction gameAction)
        {
            var actionInfo = 
                ActionInfos.FirstOrDefault(x => x.GameAction.Guid == gameAction.Guid);

            if (actionInfo == null) return;

            actionInfo.Happened = true;

            _ObjectivesChannel.OnObjectiveProgress(this);
            
            if (ActionInfos.Any(x => !x.Happened)) return;
            
            _ObjectivesChannel.OnActionEvent -= OnActionEvent;
            
            this.Complete();
        }
    }
}