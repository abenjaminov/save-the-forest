using _Scripts.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class PlayerChangeShapeState : StateBase
    {
        private PlayerVisuals _PlayerVisuals;
        private PlayerMovement _playerMovment;
        private PlayerAudio _playerAudio;
        public PlayerShape Shape;
        private static readonly int s_Shape = Animator.StringToHash("Shape");

        public PlayerChangeShapeState(AnimatorController animator, PlayerVisuals playerVisuals, PlayerMovement playerMovement, PlayerAudio playerAudio) : base(animator)
        {
            _PlayerVisuals = playerVisuals;
            _playerMovment = playerMovement;
            _playerAudio = playerAudio;
            Shape = _PlayerVisuals.CurrentShape;
        }

        protected override AnimationStateEnum GetAnimationState()
        {
            return AnimationStateEnum.None;
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
            _playerAudio.PlayShiftEffect();
            _animator.Refresh();
            _playerMovment.RefreshCharatercontroller(_PlayerVisuals.CurrentVisuals);

        }
    }
}