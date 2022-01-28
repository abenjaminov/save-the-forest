using _Scripts.ScriptableObjects.Objectives;
using _Scripts.UI.Models;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "UI Channel", menuName = "Channels/UI Channel", order = 0)]
    public class UIChannel : ScriptableObject
    {
        public UnityAction<Hint> OnShowHintEvent;
        public UnityAction<Hint> OnHideHintEvent;

        public void OnShowHint(Hint hint)
        {
            OnShowHintEvent?.Invoke(hint);
        }

        public void OnHideHint(Hint hint)
        {
            OnHideHintEvent?.Invoke(hint);
        }
    }
}