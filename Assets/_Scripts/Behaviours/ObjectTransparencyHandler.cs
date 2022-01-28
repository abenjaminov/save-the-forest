using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Scripts.Behaviours
{
    public class ObjectTransparencyHandler : MonoBehaviour
    {
        [SerializeField] private Transform _firstEdge;
        [SerializeField] private Transform _secondEdge;
        [Range(0,1)] [SerializeField] private float _transparency;

        private List<MeshRenderer> _objectsBetween;
        private static readonly int s_Transparency = Shader.PropertyToID("_Transparency");

        private void Awake()
        {
            _objectsBetween = new List<MeshRenderer>();
        }

        private void Update()
        {
            var firstEdgePosition = _firstEdge.position;
            var secondEdgePosition = _secondEdge.position;

            var direction = (secondEdgePosition - firstEdgePosition).normalized;

            if (_objectsBetween.Count > 0)
            {
                _objectsBetween.ForEach(x => x.shadowCastingMode = ShadowCastingMode.On);
            }
            
            var hits = new RaycastHit[100];
            var size = Physics.BoxCastNonAlloc(firstEdgePosition, new Vector3(5,5,100),  direction, hits);
            
            if (size > 0)
            {
                _objectsBetween = hits.Where(x => x.collider != null && 
                                                  x.collider.gameObject != _firstEdge.gameObject && 
                                                  x.collider.gameObject != _secondEdge.gameObject)
                    .Select(x => x.collider.GetComponent<MeshRenderer>()).ToList();
                _objectsBetween.ForEach(x => x.shadowCastingMode = ShadowCastingMode.ShadowsOnly);
            }
        }
    }
}