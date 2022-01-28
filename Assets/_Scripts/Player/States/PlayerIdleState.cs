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

        protected override AnimationState GetAnimationState()
        {
            return AnimationState.Idle;
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