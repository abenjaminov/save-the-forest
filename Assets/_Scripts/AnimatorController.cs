using UnityEngine;

namespace _Scripts
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public Animator Getanimator()
        {
            return _animator;
        }

        public bool HasAnimator()
        {
            return _animator != null;
        }
    }
}