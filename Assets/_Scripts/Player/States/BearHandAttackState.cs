using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class BearHandAttackState : StateBase
    {        
        public BearHandAttackState(AnimatorController animator) : base(animator)
        {
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.Attack1;
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}