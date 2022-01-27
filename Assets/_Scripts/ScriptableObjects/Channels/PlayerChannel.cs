using _Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 2)]
    public class PlayerChannel : ScriptableObject
    {
        public UnityAction<PlayerShape> PlayerChangeShapeEvent;

        public void OnPlayerChangeShape(PlayerShape shape)
        {
            PlayerChangeShapeEvent?.Invoke(shape);
        }
    }
}