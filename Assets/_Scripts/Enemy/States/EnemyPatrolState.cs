using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateBase
{
    private EnemyMovement _EnemyMovement;

    public EnemyPatrolState(AnimatorController animator, EnemyMovement EnemyMovement) : base(animator)
    {
        _EnemyMovement = EnemyMovement;
    }

    protected override AnimationStateEnum GetAnimationState()
    {
        return AnimationStateEnum.Run;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _EnemyMovement.ResetNavigation();
    }

    public override void Tick()
    {
        _EnemyMovement.Patrol();
    }

    public override void OnExit()
    {

    }
}
