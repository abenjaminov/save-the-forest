using System;
using System.Collections.Generic;
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
            
            ChangeShape(CurrentShape);
        }

        public void ChangeShape(PlayerShape shape)
        {
            //_changeShapeEffect.Play();
            
            ShapesMap[CurrentShape].SetActive(false);
            CurrentShape = shape;
            ShapesMap[CurrentShape].SetActive(true);
            
            switch (shape)
            {
                case PlayerShape.Human:
                    _currentVisuals = HumanVisuals;
                    break;
                case PlayerShape.Bear:
                    _currentVisuals = BearVisuals;
                    break;
                case PlayerShape.Rabbit:
                    _currentVisuals = RabbitVisuals;
                    break;
            }
            
            _currentVisuals.SetActive(true);
        }
    }
}