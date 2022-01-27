using _Scripts.State.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        Run
    }

    public abstract class StateBase : IState
    {
        protected Animator _animator;
        private static readonly int s_State = Animator.StringToHash("_State");

        protected StateBase(Animator animator)
        {
            _animator = animator;
        }

        protected abstract AnimationState GetAnimationState();

        public abstract void Tick();

        public virtual void OnEnter()
        {
            if (_animator == null) return;

            _animator.SetInteger(s_State, (int)GetAnimationState());
        }

        public abstract void OnExit();
    }
}
