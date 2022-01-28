using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerMoveState : StateBase
    {
        public Vector2 MovementDirection;
        private PlayerMovement _PlayerMovement;
        
        public PlayerMoveState(AnimatorController animator, PlayerMovement playerMovement) : base(animator)
        {
            _PlayerMovement = playerMovement;
        }

        protected override AnimationState GetAnimationState()
        {
            return AnimationState.Run;
        }

        public override void Tick() 
        {
            _PlayerMovement.Move(MovementDirection);
        }

        public override void OnExit()
        {
            
        }
    }
}