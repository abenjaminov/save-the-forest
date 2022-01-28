using _Scripts.ScriptableObjects.Objectives;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Objectives Channel", menuName = "Channels/Objectives", order = 0)]
    public class ObjectivesChannel : ScriptableObject
    {
        public UnityAction<string> OnEnterLocationEvent;
        public UnityAction<Objective> OnObjectiveCompleteEvent;
        public UnityAction<Objective> OnObjectiveActiveEvent;

        
        
        public void OnObjectiveComplete(Objective obj)
        {
            OnObjectiveCompleteEvent?.Invoke(obj);
        }

        public void OnEnterLocation(string locationGuid)
        {
            OnEnterLocationEvent?.Invoke(locationGuid);
        }

        public void OnObjectiveActive(Objective objective)
        {
            OnObjectiveActiveEvent?.Invoke(objective);
        }
    }
}