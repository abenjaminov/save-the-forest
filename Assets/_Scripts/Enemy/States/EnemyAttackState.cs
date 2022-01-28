using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : StateBase
{
    private Combat _Combat;
    EnemyMovement _EnemyMovement;

    public EnemyAttackState(AnimatorController animator, EnemyMovement enemyMovement, Combat combat) : base(animator)
    {
        _Combat = combat;
        _EnemyMovement = enemyMovement;
    }

    protected override AnimationStateEnum GetAnimationState()
    {
        return AnimationStateEnum.Attack1;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _Combat.RepeatedAttack();
        _EnemyMovement.Idle();
    }

    public override void Tick()
    {
    }

    public override void OnExit()
    {
        _Combat.StopRepeatedAttack();
    }
}
