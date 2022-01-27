using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerIdleState : PlayerStateBase
    {
        private PlayerMovement _PlayerMovement;
        
        public PlayerIdleState(AnimatorController animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override PlayerAnimationState GetAnimationState()
        {
            return PlayerAnimationState.Idle;
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