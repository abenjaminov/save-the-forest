using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : StateBase
{
    private Combat _Combat;

    public EnemyAttackState(AnimatorController animator, Combat combat) : base(animator)
    {
        _Combat = combat;
    }

    protected override AnimationStateEnum GetAnimationState()
    {
        return AnimationStateEnum.Attack1;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _Combat.Attack();
    }

    public override void Tick()
    {
    }

    public override void OnExit()
    {
    }
}
