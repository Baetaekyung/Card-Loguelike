using Unity.Collections;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "OJYSO/AnimationParameter", order = int.MinValue)]
    public class AnimationParameterSO : ScriptableObject
    {
        [SerializeField] private string parameterName;
        [SerializeField] private int hashValue;
        public int GetHashValue => hashValue;
        private void OnValidate()
        {
            hashValue = Animator.StringToHash(parameterName);
        }
    }
}
