using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerChangeShapeState : PlayerStateBase
    {
        private PlayerVisuals _PlayerVisuals;
        public PlayerShape Shape;
        private static readonly int s_Shape = Animator.StringToHash("Shape");

        public PlayerChangeShapeState(AnimatorController animator, PlayerVisuals playerVisuals) : base(animator)
        {
            _PlayerVisuals = playerVisuals;
            Shape = PlayerShape.Human;
        }

        protected override PlayerAnimationState GetAnimationState()
        {
            return PlayerAnimationState.None;
        }

        public override void OnEnter()
        {
            _PlayerVisuals.ChangeShape(Shape);
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}