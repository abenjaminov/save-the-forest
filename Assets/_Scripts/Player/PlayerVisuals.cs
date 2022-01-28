using System;
using System.Collections.Generic;
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
        [SerializeField] private ParticleSystem _changeShapeEffect;

        [SerializeField] private GameObject HumanVisuals;
        [SerializeField] private GameObject BearVisuals;
        [SerializeField] private GameObject RabbitVisuals;

        private Dictionary<PlayerShape, GameObject> ShapesMap = new Dictionary<PlayerShape, GameObject>();
        
        private GameObject _currentVisuals;
        public PlayerShape CurrentShape;

        private void Awake()
        {
            ShapesMap.Add(PlayerShape.Human, HumanVisuals);
            ShapesMap.Add(PlayerShape.Bear, BearVisuals);
            ShapesMap.Add(PlayerShape.Rabbit, RabbitVisuals);

            _currentVisuals = ShapesMap[CurrentShape];
        }

        public void ChangeShape(PlayerShape shape)
        {
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