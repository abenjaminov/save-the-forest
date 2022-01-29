using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects.Channels;
using CartoonFX;
using UnityEngine;

namespace _Scripts.Player
{
    public enum PlayerShape
    {
        Human,
        Bear, 
        Rabbit
    }
    
    [Serializable]
    public class PlayershapeInfo
    {
        public PlayerShape Shape;
        public GameObject Visuals;
        public Vector3 Center;
        public float Radius;
        public float Height;
        public float moveSpeed;
        public float jumpSpeed;
    }

    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private ParticleSystem _changeShapeEffect;

        [SerializeField] private PlayershapeInfo HumanVisuals;
        [SerializeField] private PlayershapeInfo BearVisuals;
        [SerializeField] private PlayershapeInfo RabbitVisuals;

        private Dictionary<PlayerShape, PlayershapeInfo> ShapesMap = new Dictionary<PlayerShape, PlayershapeInfo>();
        
        [HideInInspector] public PlayershapeInfo CurrentVisuals;
        public PlayerShape CurrentShape;

        [SerializeField] private bool _canChangeToBear;
        [SerializeField] private bool _canChangeToRabbit;
        
        private void Awake()
        {
            _GameChannel.OnReceiveBearAbilityEvent += OnReceiveBearAbilityEvent;
            _GameChannel.OnReceiveRabbitAbilityEvent += OnReceiveRabbitAbilityEvent;
            ShapesMap.Add(PlayerShape.Human, HumanVisuals);
            ShapesMap.Add(PlayerShape.Bear, BearVisuals);
            ShapesMap.Add(PlayerShape.Rabbit, RabbitVisuals);

            CurrentVisuals = ShapesMap[CurrentShape];
            _canChangeToBear = false;
            _canChangeToRabbit = false;

        }

        private void OnReceiveRabbitAbilityEvent()
        {
            _canChangeToRabbit = true;
        }

        private void OnReceiveBearAbilityEvent()
        {
            _canChangeToBear = true;
        }

        public void ChangeShape(PlayerShape shape)
        {
            if (!_canChangeToBear && shape == PlayerShape.Bear ||
                !_canChangeToRabbit && shape == PlayerShape.Rabbit) return;
            
            _changeShapeEffect.Play(true);
            
            ShapeShift(shape);
        }

        private void ShapeShift(PlayerShape shape)
        {
            ShapesMap[CurrentShape].Visuals.SetActive(false);
            CurrentShape = shape;
            ShapesMap[CurrentShape].Visuals.SetActive(true);
            CurrentVisuals = ShapesMap[CurrentShape];
        }
    }
}