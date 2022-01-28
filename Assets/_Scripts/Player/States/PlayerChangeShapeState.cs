using _Scripts.State;
using UnityEngine;
using AnimationState = _Scripts.State.AnimationState;

namespace _Scripts.Player.States
{
    public class PlayerChangeShapeState : StateBase
    {
        private PlayerVisuals _PlayerVisuals;
        public PlayerShape Shape;
        private static readonly int s_Shape = Animator.StringToHash("Shape");

        public PlayerChangeShapeState(AnimatorController animator, PlayerVisuals playerVisuals) : base(animator)
        {
            _PlayerVisuals = playerVisuals;
            Shape = PlayerShape.Human;
        }

        protected override AnimationState GetAnimationState()
        {
            return AnimationState.None;
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