using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetTarget(GameObject.Find("player").transform.position);
    }

    public void SetTarget(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
