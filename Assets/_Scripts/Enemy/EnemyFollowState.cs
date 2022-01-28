using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : StateBase
{
    private EnemyMovement _EnemyMovement;
    public Transform target;

    public EnemyFollowState(AnimatorController animator, EnemyMovement EnemyMovement) : base(animator)
    {
        _EnemyMovement = EnemyMovement;
        target = GameObject.Find("Player").transform;
    }

    protected override AnimationStateEnum GetAnimationState()
    {
        return AnimationStateEnum.Run;
    }

    public bool TargetVisible(Vector3 pos, float distance)
    {
        RaycastHit hit;
        if (Vector3.Distance(pos, target.position) <= distance / 1.5f)
        {
            return true;
        }
        else if (Vector3.Distance(pos, target.position) <= distance)
        {
            Physics.Raycast(pos, target.position, out hit);
            return hit.Equals(target);
        }
        return false;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Tick()
    {
        _EnemyMovement.Follow(target);
    }

    public override void OnExit()
    {

    }
}
