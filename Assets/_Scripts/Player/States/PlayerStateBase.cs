using _Scripts.State.State;
using UnityEngine;

namespace _Scripts.Player.States
{
    public enum PlayerAnimationState
    {
        Idle,
        Run,
        Jump
    }
    
    public abstract class PlayerStateBase :IState
    {
        protected Animator _animator;
        private static readonly int s_State = Animator.StringToHash("_State");

        protected PlayerStateBase(Animator animator)
        {
            _animator = animator;
        }

        protected abstract PlayerAnimationState GetAnimationState();
        
        public abstract void Tick();

        public virtual void OnEnter()
        {
            if (_animator == null) return;
            
            _animator.SetInteger(s_State, (int)GetAnimationState());
        }

        public abstract void OnExit();
    }
}