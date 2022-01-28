using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class BearHandAttackState : StateBase
    {
        Combat _combat;
        public BearHandAttackState(AnimatorController animator, Combat combat) : base(animator)
        {
            _combat = combat;
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.Attack1;
        }

        public override void OnEnter()
        {
            _combat.AttackInSeconds();
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}