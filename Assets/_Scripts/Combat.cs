using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float Damage;
    public float AttackRadius;
    
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
            if(c.TryGetComponent<Health>(out h))
            {
                h.Hit(Damage);
            }
        }
    }
}
