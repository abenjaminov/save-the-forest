using _Scripts;
using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        print("attack");
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
