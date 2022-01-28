using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class BearFrontAttackState : StateBase
    {
        public BearFrontAttackState(AnimatorController animator) : base(animator)
        {
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.Attack2;
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}