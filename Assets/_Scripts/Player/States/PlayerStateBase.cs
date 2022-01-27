using _Scripts.State.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public enum PlayerAnimationState
    {
        None,
        Idle,
        Run,
        Jump
    }
    
    public abstract class PlayerStateBase :IState
    {
        protected AnimatorController _animator;
        private static readonly int s_State = Animator.StringToHash("_State");

        protected PlayerStateBase(AnimatorController animator)
        {
            _animator = animator;
        }

        protected abstract PlayerAnimationState GetAnimationState();
        
        public abstract void Tick();

        public virtual void OnEnter()
        {
            if (!_animator.HasAnimator()) return;

            _animator.Getanimator().SetInteger(s_State, (int)GetAnimationState());
        }

        public abstract void OnExit();
    }
}