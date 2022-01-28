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
    public string FilterTag = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, AttackRadius);
        foreach(var c in hits)
        {
            Health h;
            if(c.TryGetComponent<Health>(out h) && (FilterTag == "" || c.tag != FilterTag))
            {
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
}
