using CardGame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <class name ="SkillCard">
/// ��ųī��� ��� ��밡���� ī��
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
    /// Ű��������Ʈ �Ŵ����� ������ �ִ� ��� Ű������ �̺�Ʈ�� �ϳ��� ���ϸ鼭
    /// ���� Ű���尡 �����Ѵٸ� �� Ű������ ȿ���� �������ش�.
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

                    Debug.Log($"{effect.keyword} Ű���� ����");
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
