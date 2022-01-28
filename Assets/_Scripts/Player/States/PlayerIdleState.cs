using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerIdleState : StateBase
    {
        private PlayerMovement _PlayerMovement;
        
        public PlayerIdleState(AnimatorController animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.Idle;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _PlayerMovement.Idle();
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}