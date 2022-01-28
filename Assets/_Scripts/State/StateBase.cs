using _Scripts.State.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : MonoBehaviour
{
    public enum AnimationState
    {
        None,
        Idle,
        Run,
        Jump
    }

    public abstract class StateBase : IState
    {
        protected AnimatorController _animator;
        private static readonly int s_State = Animator.StringToHash("_State");

        protected StateBase(AnimatorController animator)
        {
            _animator = animator;
        }

        protected abstract AnimationState GetAnimationState();

        public abstract void Tick();

        public virtual void OnEnter()
        {
            if (!_animator.HasAnimator()) return;

            _animator.Getanimator().SetInteger(s_State, (int)GetAnimationState());
        }

        public abstract void OnExit();
    }
}
