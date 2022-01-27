using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public class ObjectTransparencyHandler : MonoBehaviour
    {
        [SerializeField] private Transform _firstEdge;
        [SerializeField] private Transform _secondEdge;
        [Range(0,1)] [SerializeField] private float _transparency;

        private List<Renderer> _objectsBetween;
        private static readonly int s_Transparency = Shader.PropertyToID("_Transparency");

        private void Awake()
        {
            _objectsBetween = new List<Renderer>();
        }

        private void Update()
        {
            var firstEdgePosition = _firstEdge.position;
            var secondEdgePosition = _secondEdge.position;
            
            
            var direction = secondEdgePosition - firstEdgePosition;
            var ray = new Ray(firstEdgePosition, direction);
            Debug.DrawRay(firstEdgePosition, direction, Color.black);
            var hits = Physics.RaycastAll(firstEdgePosition, direction, Vector3.Distance(secondEdgePosition, firstEdgePosition));

            if (_objectsBetween.Count > 0)
            {
                _objectsBetween.ForEach(x => x.material.SetFloat(s_Transparency, 1));
            }
            
            if (hits.Length > 0)
            {
                _objectsBetween = hits.Select(x => x.collider.GetComponent<Renderer>()).ToList();
                _objectsBetween.ForEach(x => x.material.SetFloat(s_Transparency, _transparency));
            }
        }
    }
}