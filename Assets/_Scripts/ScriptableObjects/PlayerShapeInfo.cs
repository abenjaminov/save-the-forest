using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Shape", menuName = "PlayerShape", order = 0)]
    public class PlayerShapeInfo : ScriptableObject
    {
        public GameObject PlayerShapeVisuals;
    }
}