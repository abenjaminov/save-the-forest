using _Scripts.Player;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts
{
    public class DebugHelper : MonoBehaviour
    {
        [SerializeField] private PlayerChannel _PlayerChannel;
        
        private static DebugHelper HelperInstance;

        private void Awake()
        {
            HelperInstance = this;
        }

        [UnityEditor.MenuItem("Game Actions/Shape Shift/Bear")]
        public static void ChangeToBear()
        { 
            HelperInstance._PlayerChannel.OnPlayerChangeShape(PlayerShape.Bear);
        }
    }
}