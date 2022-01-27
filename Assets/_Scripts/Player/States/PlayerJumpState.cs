using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerJumpState : PlayerStateBase
    {
        private PlayerMovement _PlayerMovement;
        
        public PlayerJumpState(AnimatorController animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override PlayerAnimationState GetAnimationState()
        {
            return PlayerAnimationState.Jump;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _PlayerMovement.Jump();
        }
        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}