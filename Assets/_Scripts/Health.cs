using _Scripts.ScriptableObjects.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject
{
    public Health ObjectHealth;
    public float Damage;

    public HitObject(Health health, float damage)
    {
        ObjectHealth = health;
        Damage = damage;
    }
}

public class Health : MonoBehaviour
{
    public float HP;
    [SerializeField]
    CombatChannel _combatChannel;
    public bool invincible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float hit(float damage)
    {
        if (!invincible)
        {
            HP -= damage;
            if (HP <= 0)
            {
                OnDeath();
                HP = 0;
            }
        }
        else damage = 0;
        _combatChannel.OnHit(new HitObject(this, damage));
        return HP;
    }

    public void OnDeath()
    {
        _combatChannel.OnDeath(this);
    }
}
