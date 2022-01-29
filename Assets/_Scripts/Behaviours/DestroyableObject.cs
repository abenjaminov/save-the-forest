using System;
using System.Collections;
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

        [SerializeField] private ParticleSystem _DestroyedEffect;
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
            if (_DestroyedEffect != null)
            {
                _DestroyedEffect.Play(true);
                yield return new WaitForSeconds(_DestroyedEffect.main.duration);
            }
            
            Destroy(gameObject);
        }
    }
}