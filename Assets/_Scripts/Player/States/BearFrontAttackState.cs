using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class BearFrontAttackState : StateBase
    {
        Combat _combat;
        public BearFrontAttackState(AnimatorController animator, Combat combat) : base(animator)
        {
            _combat = combat;
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.Attack2;
        }

        public override void Tick()
        {
            _combat.AttackInSeconds();
        }

        public override void OnExit()
        {
            
        }
    }
}