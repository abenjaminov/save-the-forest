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
    
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private ParticleSystem _changeShapeEffect;

        [SerializeField] private GameObject HumanVisuals;
        [SerializeField] private GameObject BearVisuals;
        [SerializeField] private GameObject RabbitVisuals;

        private Dictionary<PlayerShape, GameObject> ShapesMap = new Dictionary<PlayerShape, GameObject>();
        
        private GameObject _currentVisuals;
        public PlayerShape CurrentShape;

        [SerializeField] private bool _canChangeToBear;
        [SerializeField] private bool _canChangeToRabbit;
        
        private void Awake()
        {
            _GameChannel.OnReceiveBearAbilityEvent += OnReceiveBearAbilityEvent;
            ShapesMap.Add(PlayerShape.Human, HumanVisuals);
            ShapesMap.Add(PlayerShape.Bear, BearVisuals);
            ShapesMap.Add(PlayerShape.Rabbit, RabbitVisuals);

            _canChangeToBear = false;
            _canChangeToRabbit = false;
            
            _currentVisuals = ShapesMap[CurrentShape];
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
            ShapesMap[CurrentShape].SetActive(false);
            CurrentShape = shape;
            ShapesMap[CurrentShape].SetActive(true);
        }
    }
}