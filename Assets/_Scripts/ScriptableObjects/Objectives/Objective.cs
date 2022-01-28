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
        [SerializeField] protected ObjectivesChannel _ObjectivesChannel;
        [SerializeField] protected string Description;
        protected ObjectiveState State;

        protected void Complete()
        {
            State = ObjectiveState.Complete;
            _ObjectivesChannel.OnObjectiveComplete(this);
        }
    }
}