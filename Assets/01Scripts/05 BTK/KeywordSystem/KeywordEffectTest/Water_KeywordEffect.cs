using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "Water_KeywordEffect", menuName = "SO/Effect/KeywordEffect")]
    public class Water_KeywordEffect : KeywordEffectSO
    {
        protected override void Effect(int keywordCount)
        {
            Debug.Log($"{keywordCount}의 Water 효과 실행");
        }
    }
}
