using UnityEngine;
using _Scripts.Player;

namespace _Scripts.ScriptableObjects.Channels.Input.Models
{
    public class InputActionOptions
    {
        public Vector2 MovementDirection;
        public bool isAttack1Pressed;
        public bool isAttack2Pressed;
        public PlayerShape PlayerShape;
    }
}