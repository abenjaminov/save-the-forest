using System;
using _Scripts;
using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private CombatChannel _CombatChannel;
    
    private Health _health;
    public string AttackName = "Attack";
    public float Damage;
    public float AttackRadius;
    public float AttackInterval;
    public float FirstAttackDelay;
    public float DirMultiplier;
    public string FilterTag = "";
    bool showAttack = false;
    public bool ShowAttackOutline;
    public float Knockback;

    public bool _canAttack;

    private void OnDestroy()
    {
        _CombatChannel.DeathEvent -= DeathEvent;
    }

    private void Awake()
    {
        _canAttack = true;
        _health = GetComponent<Health>();
        _CombatChannel.DeathEvent += DeathEvent;
    }

    private void DeathEvent(Health arg0)
    {
        if(arg0.gameObject != this.gameObject || arg0.HP > 0) return;
        
        _canAttack = false;
    }

    public void Attack()
    {
        if (!_canAttack) return;
        
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * DirMultiplier
            , AttackRadius);
        showAttack = true;
        foreach(var c in hits)
        {
            Health h;
            if(c.TryGetComponent<Health>(out h) && (FilterTag == "" || c.tag != FilterTag))
            {
                if(h.enabled)
                    h.Hit(Damage);
            }
        }
    }

    public void AttackInSeconds()
    {
        Invoke("Attack", FirstAttackDelay);
    }

    public void RepeatedAttack()
    {
        InvokeRepeating("Attack", FirstAttackDelay, AttackInterval);
    }

    public void StopRepeatedAttack()
    {
        CancelInvoke();
    }
    public void OnDrawGizmos()
    {
        if (showAttack && Application.isEditor && ShowAttackOutline)
        {
            Gizmos.DrawSphere(transform.position
            + transform.forward * DirMultiplier, AttackRadius);
            showAttack = false;
        }
    }
}
