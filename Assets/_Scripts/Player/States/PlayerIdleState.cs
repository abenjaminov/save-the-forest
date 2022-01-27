using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerIdleState : PlayerStateBase
    {
        private PlayerMovement _PlayerMovement;
        
        public PlayerIdleState(Animator animator, PlayerMovement _playerMovement) : base(animator)
        {
        }

        protected override PlayerAnimationState GetAnimationState()
        {
            return PlayerAnimationState.Idle;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _PlayerMovement.SetSpeed(0);
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}