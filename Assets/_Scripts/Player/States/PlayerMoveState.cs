using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerMoveState : PlayerStateBase
    {
        public Vector2 MovementDirection;
        private PlayerMovement _PlayerMovement;
        
        public PlayerMoveState(Animator animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override PlayerAnimationState GetAnimationState()
        {
            return PlayerAnimationState.Run;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _PlayerMovement.Move(MovementDirection);
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}