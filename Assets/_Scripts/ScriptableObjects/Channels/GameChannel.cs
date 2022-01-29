using _Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Game Channel", menuName = "Channels/Game Channel", order = 2)]
    public class GameChannel : ScriptableObject
    {
        public UnityAction<GameAction> OnActionEvent;
        public UnityAction<StoryItem> OnShowStoryEvent;
        public UnityAction<StoryItem> OnStoryToldEvent;
        public UnityAction OnReceiveBearAbilityEvent;

        public void OnReceiveBearAbility()
        {
            OnReceiveBearAbilityEvent?.Invoke();
        }
        
        public void OnStoryTold(StoryItem storyItem)
        {
            OnStoryToldEvent?.Invoke(storyItem);
        }

        public void OnShowStory(StoryItem storyItem)
        {
            OnShowStoryEvent?.Invoke(storyItem);
        }
        
        public void OnAction(GameAction gameAction)
        {
            OnActionEvent?.Invoke(gameAction);
        }
    }
}