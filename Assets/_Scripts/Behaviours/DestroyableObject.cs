using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private GameAction OnDestroyedAction;
    }
}