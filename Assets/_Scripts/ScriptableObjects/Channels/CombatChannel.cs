using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/CombatChannel", order = 3)]
    public class CombatChannel : ScriptableObject
    {
        public UnityAction<HitObject> HitEvent;
        public UnityAction<Health> DeathEvent;

        public void OnHit(HitObject hitObject)
        {
            HitEvent?.Invoke(hitObject);
        }

        public void OnDeath(Health health)
        {
            DeathEvent?.Invoke(health);
        }
    }
}