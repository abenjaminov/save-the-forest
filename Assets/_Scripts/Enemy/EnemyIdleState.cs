using _Scripts;
using System.Collections;
using System.Collections.Generic;
using _Scripts.State;
using UnityEngine;

public class EnemyIdleState : StateBase
{
    private EnemyMovement _EnemyMovement;

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

        _EnemyMovement.Idle();
    }

    public override void Tick()
    {

    }

    public override void OnExit()
    {

    }
}
