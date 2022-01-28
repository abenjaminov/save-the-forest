using System.Linq;
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
        
        public UnityAction<GameAction> OnActionEvent;
        public UnityAction<ActionObjective> OnObjectiveCompleteEvent;
        public UnityAction<ActionObjective> OnObjectiveActiveEvent;

        public void OnObjectiveComplete(ActionObjective obj)
        {
            OnObjectiveCompleteEvent?.Invoke(obj);
            _UIChannel.OnHideHintEvent(obj.GUID);
        }

        public void OnAction(GameAction gameAction)
        {
            OnActionEvent?.Invoke(gameAction);
        }

        public void OnObjectiveActive(ActionObjective objective)
        {
            OnObjectiveActiveEvent?.Invoke(objective);

            UpdateHints(objective);
        }

        private void UpdateHints(ActionObjective objective)
        {
            foreach (var actionInfo in objective.ActionInfos)
            {
                if (actionInfo.Happened)
                {
                    _UIChannel.OnHideHintEvent(actionInfo.GameAction.Guid);
                }
                else
                {
                    _UIChannel.OnShowHintEvent(new Hint()
                    {
                        HintGuid = actionInfo.GameAction.Guid,
                        HintText = actionInfo.GameAction.Hint
                    });
                }
            }
        }

        public void OnObjectiveProgress(ActionObjective actionObjective)
        {
            UpdateHints(actionObjective);
        }
    }
}