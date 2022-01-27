using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public List<Transform> PatrolRoute;
    public int PatrolRouteIndex = -1;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTargetPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public void SetFollow(Transform t)
    {
        ResetNavigation();
        Target = t;
    }

    public void SetIdle()
    {
        ResetNavigation();
        SetTargetPosition(transform.position);
    }

    public void Patrol()
    {
        Target = null;
        Vector3 closestPos = PatrolRoute[0].position;
        var minDistance = Mathf.Infinity;
        var index = 0;

        if (PatrolRouteIndex == -1)
        {
            //Get closest point
            foreach (var t in PatrolRoute)
            {
                var dist = Vector3.Distance(transform.position, t.position);
                if (dist < 0.3f)
                {
                    minDistance = dist;
                    break;
                }
                else if (dist < minDistance)
                {
                    minDistance = dist;
                    closestPos = t.position;
                }
                index++;
            }
        }
        else
        {
            minDistance = Vector3.Distance(transform.position, PatrolRoute[PatrolRouteIndex].position);
        }
        print(minDistance);
        print(index);

        //if arrived start patrol, move to next
        //if patrolling go to next point until touching and then go to next
        if (minDistance < 0.3)
        {
            if (PatrolRouteIndex == -1)
                PatrolRouteIndex = index;

            if (PatrolRouteIndex + 1 == PatrolRoute.Count)
                PatrolRouteIndex = 0;
            else 
                PatrolRouteIndex += 1;

            SetTargetPosition(PatrolRoute[PatrolRouteIndex].position);
        }
        else if(PatrolRouteIndex == -1)
            SetTargetPosition(closestPos);
    }

    public void ResetNavigation()
    {
        Target = null;
        PatrolRouteIndex = -1;
    }

    public void LookAt(Transform t)
    {
        transform.LookAt(t);
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            SetTargetPosition(Target.position);
        }
        Patrol();
    }
}
