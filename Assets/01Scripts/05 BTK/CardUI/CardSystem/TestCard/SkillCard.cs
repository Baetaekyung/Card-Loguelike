using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <class name ="SkillCard">
/// Skill card is the base of every useable card 
/// it will be GetSkill, or buff to player
/// </class> 
/// </summary>
public class SkillCard : CardObject
{
    [SerializeField] private KeywordEffectSOList _keywordEffectList;
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
    ///KeywordEffectSOList has keywordEffectSO
    ///keywordEffectSO has keyword and UnityEvent, Excute all Event
    ///if card have the keyword excute the keywordEffect
    /// </function>
    /// </summary>
    protected virtual void KeywordEffectExcute()
    {
        for (int i = 0; i < _keywords.Count; i++)
        {

            foreach (var effect in _keywordEffectList.keywordEffects)
            {
                if (effect.keyword == _keywords[i])
                {
                    Debug.Log($"{_keywords[i]} event Invoked");
                    effect.ExcuteEffect();
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
