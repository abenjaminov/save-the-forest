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

            var direction = (secondEdgePosition - firstEdgePosition).normalized;

            if (_objectsBetween.Count > 0)
            {
                _objectsBetween.ForEach(x => x.material.SetFloat(s_Transparency, 1));
            }
            
            var hits = new RaycastHit[100];
            var size = Physics.RaycastNonAlloc(firstEdgePosition, direction, hits, Vector3.Distance(secondEdgePosition, firstEdgePosition));
            
            if (size > 0)
            {
                _objectsBetween = hits.Where(x => x.collider != null && 
                                                  x.collider.gameObject != _firstEdge.gameObject && 
                                                  x.collider.gameObject != _secondEdge.gameObject)
                    .Select(x => x.collider.GetComponent<Renderer>()).ToList();
                _objectsBetween.ForEach(x => x.material.SetFloat(s_Transparency, _transparency));
            }
        }
    }
}