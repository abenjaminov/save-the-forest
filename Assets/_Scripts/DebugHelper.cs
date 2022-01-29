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

        #if UNITY_EDITOR
        
        [UnityEditor.MenuItem("Game Actions/Shape Shift/Human")]
        public static void ChangeToHuman()
        { 
            HelperInstance._PlayerChannel.OnPlayerChangeShape(PlayerShape.Human);
        }
        
        [UnityEditor.MenuItem("Game Actions/Shape Shift/Bear")]
        public static void ChangeToBear()
        { 
            HelperInstance._PlayerChannel.OnPlayerChangeShape(PlayerShape.Bear);
        }
        
        [UnityEditor.MenuItem("Game Actions/Shape Shift/Rabbit")]
        public static void ChangeToRabbit()
        { 
            HelperInstance._PlayerChannel.OnPlayerChangeShape(PlayerShape.Rabbit);
        }
        
        #endif
    }
}