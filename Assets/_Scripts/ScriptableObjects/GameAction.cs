using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Action", menuName = "GameAction", order = 0)]
    public class GameAction : ScriptableObject
    {
        public string Name;
        public string Hint;
        public string Guid;
    }
}