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
    
    public abstract class Objective : ScriptableObject
    {
        public string GUID;
        [SerializeField] protected ObjectivesChannel _ObjectivesChannel;
        public string Description;
        public Objective NextObjective;
        protected ObjectiveState State;

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
    }
}