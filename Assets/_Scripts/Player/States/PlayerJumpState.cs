using _Scripts.State;
using UnityEngine;
using AnimationState = _Scripts.State.AnimationState;

namespace _Scripts.Player.States
{
    public class PlayerJumpState : StateBase
    {
        private PlayerMovement _PlayerMovement;
        
        public PlayerJumpState(AnimatorController animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override AnimationState GetAnimationState()
        {
            return AnimationState.Jump;
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