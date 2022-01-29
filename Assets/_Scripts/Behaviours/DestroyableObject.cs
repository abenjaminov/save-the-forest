using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(Collider))]
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private GameObject _Visuals;
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private GameAction OnDestroyedAction;

        [SerializeField] private CombatChannel _CombatChannel;
        [SerializeField] private Health _healthComponent;

        [SerializeField] private List<ParticleSystem> _DestroyedEffects;
        private Collider _collider;
        
        private void Awake()
        {
            _CombatChannel.DeathEvent += DeathEvent;
            _collider = GetComponent<Collider>();
        }

        private void DeathEvent(Health arg0)
        {
            if (arg0 != _healthComponent) return;

            _GameChannel.OnAction(OnDestroyedAction);

            StartCoroutine(DestorySequence());
        }

        IEnumerator DestorySequence()
        {
            _collider.enabled = false;
            _Visuals.SetActive(false);
            if (_DestroyedEffects != null && _DestroyedEffects.Count > 0)
            {
                var duration = _DestroyedEffects.Max(x => x.main.duration);
                foreach (var effect in _DestroyedEffects)
                {
                    effect.Play(true);    
                }
                
                yield return new WaitForSeconds(duration);
            }
            
            Destroy(gameObject);
        }
    }
}