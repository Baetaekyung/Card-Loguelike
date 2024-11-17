using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace CardGame.Players
{
    public class PlayerAnimator : MonoBehaviour, IPlayerComponent
    {
        [Header("ToDisable")]
        [SerializeField] private RigTransform rigTransform;

        private Animator animator;
        public Animator GetAnimator => animator;
        public void Init(Player _player)
        {
            animator = GetComponent<Animator>();
        }
        public void SetParam(AnimationParameterSO param, bool value) => animator.SetBool(param.GetHashValue, value);
        public void SetParam(AnimationParameterSO param, float value) => animator.SetFloat(param.GetHashValue, value);
        public void SetParam(AnimationParameterSO param, float value, float damp, float deltaTime) => animator.SetFloat(param.GetHashValue, value, damp, deltaTime);
        public void SetParam(AnimationParameterSO param, int value) => animator.SetInteger(param.GetHashValue, value);
        public void SetParam(AnimationParameterSO param) => animator.SetTrigger(param.GetHashValue);
        public void Dispose(Player _player)
        {
            Destroy(rigTransform);
        }
    }
}
