using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private PlayerChannel _PlayerChannel;
        
        void OnInteract(InputValue inputAction)
        {
            _PlayerChannel.OnPlayerInteract();
        }
    }
}