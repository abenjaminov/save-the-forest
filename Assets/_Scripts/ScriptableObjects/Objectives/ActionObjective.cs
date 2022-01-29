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
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] protected ObjectivesChannel _ObjectivesChannel;
        [SerializeField] private StoryItem StoryWhenActivated;
        [SerializeField] private StoryItem StoryWhenCompleted;
        public List<ActionInfo> ActionInfos;
        public string GUID;
        
        public string Description;
        public ActionObjective NextObjective;
        protected ObjectiveState State;

        
        private void OnDisable()
        {
            _GameChannel.OnActionEvent -= OnActionEvent;
        }

        private void OnEnable()
        {
            _GameChannel.OnActionEvent += OnActionEvent;

            foreach (var actionInfo in ActionInfos)
            {
                actionInfo.Happened = false;
                actionInfo.AmountsLeft = actionInfo.RepeatAmount;
            }
        }

        public void Activate()
        {
            State = ObjectiveState.Active;
            _ObjectivesChannel.OnObjectiveActive(this);

            if (StoryWhenActivated != null)
            {
                _GameChannel.OnShowStory(StoryWhenActivated);
            }
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

            actionInfo.AmountsLeft--;

            if (actionInfo.AmountsLeft == 0)
            {
                actionInfo.Happened = true;    
            }

            _ObjectivesChannel.OnObjectiveProgress(this);
            
            if (ActionInfos.Any(x => !x.Happened)) return;
            
            _GameChannel.OnActionEvent -= OnActionEvent;

            if (StoryWhenCompleted == null)
            {
                this.Complete();    
            }
            else
            {
                _GameChannel.OnStoryToldEvent += OnStoryToldEvent;
                _GameChannel.OnShowStory(StoryWhenCompleted);
            }
        }

        private void OnStoryToldEvent(StoryItem arg0)
        {
            if (arg0.Guid != StoryWhenCompleted.Guid) return;
            
            _GameChannel.OnStoryToldEvent -= OnStoryToldEvent;
            
            this.Complete();
        }
    }
}