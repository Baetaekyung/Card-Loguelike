using CardGame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <class name ="SkillCard">
/// 스킬카드는 끌어서 사용가능한 카드
/// </class> 
/// </summary>
public class UseableCard : CardObject
{
    public List<KeywordEnum> keywords = new List<KeywordEnum>();
    [SerializeField] private int _keywordCount = 3;
    [SerializeField] private UnityEvent _skillEffects;

    protected virtual void OnEnable()
    {
        OnCardUseEvent += SkillExcute;
        OnCardUseEvent += KeywordEffectExcute;
    }

    protected virtual void SkillExcute()
    {
        //SkillEffect is only have UnityEvent excute event no condition
        _skillEffects?.Invoke();
    }

    /// <summary>
    /// <function name="KeywordEffectExcute"> 
    /// 키워드이펙트 매니저가 가지고 있는 모든 키워드의 이벤트와 하나씩 비교하면서
    /// 만약 키워드가 존재한다면 그 키워드의 효과를 실행해준다.
    /// </function>
    /// </summary>
    protected virtual void KeywordEffectExcute()
    {
        KeywordEffectManager keywordEffectM = KeywordEffectManager.Instance;

        for (int i = 0; i < keywords.Count; i++)
        {
            foreach (var effect in keywordEffectM.effectList.keywordEffects)
            {
                if (effect.keyword == keywords[i])
                {
                    if (!keywordEffectM.keywordCountDictionary.ContainsKey(keywords[i]))
                        keywordEffectM.keywordCountDictionary.Add(keywords[i], _keywordCount);
                    else if(keywordEffectM.keywordCountDictionary.ContainsKey(keywords[i]))
                        keywordEffectM.keywordCountDictionary[keywords[i]] += _keywordCount;

                    Debug.Log($"{effect.keyword} 키워드 실행");
                    effect.ExcuteEffect(keywordEffectM.keywordCountDictionary[keywords[i]]);
                }
            }
        }
    }

    protected virtual void OnDestroy()
    {
        OnCardUseEvent -= SkillExcute;
        OnCardUseEvent -= KeywordEffectExcute;
    }
}
