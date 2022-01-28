using _Scripts;
using System.Collections;
using System.Collections.Generic;
using _Scripts.State;
using UnityEngine;

public class EnemyIdleState : StateBase
{
    private EnemyMovement _EnemyMovement;
    public float IdleTime = 0;

    public EnemyIdleState(AnimatorController animator, EnemyMovement EnemyMovement) : base(animator)
    {
        _EnemyMovement = EnemyMovement;
    }

    protected override AnimationStateEnum GetAnimationState()
    {
        return AnimationStateEnum.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        IdleTime = 0;
        _EnemyMovement.Idle();
    }

    public override void Tick()
    {
        IdleTime += Time.deltaTime;
        _EnemyMovement.transform.Rotate(Vector3.up, Mathf.Sign(Mathf.Sin(IdleTime * 2)) * 0.25f);
    }

    public override void OnExit()
    {
        IdleTime = 0;
    }
}
