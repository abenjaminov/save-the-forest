using _Scripts.ScriptableObjects.Objectives;
using _Scripts.UI.Models;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Objectives Channel", menuName = "Channels/Objectives", order = 0)]
    public class ObjectivesChannel : ScriptableObject
    {
        [SerializeField] private UIChannel _UIChannel;
        
        public UnityAction<string> OnActionEvent;
        public UnityAction<Objective> OnObjectiveCompleteEvent;
        public UnityAction<Objective> OnObjectiveActiveEvent;

        public void OnObjectiveComplete(Objective obj)
        {
            OnObjectiveCompleteEvent?.Invoke(obj);
            _UIChannel.OnHideHintEvent(new Hint()
            {
                HintGuid = obj.GUID,
                HintText = obj.Description
            });
        }

        public void OnAction(string locationGuid)
        {
            OnActionEvent?.Invoke(locationGuid);
        }

        public void OnObjectiveActive(Objective objective)
        {
            OnObjectiveActiveEvent?.Invoke(objective);
            _UIChannel.OnShowHintEvent(new Hint()
            {
                HintGuid = objective.GUID,
                HintText = objective.Description
            });
        }
    }
}