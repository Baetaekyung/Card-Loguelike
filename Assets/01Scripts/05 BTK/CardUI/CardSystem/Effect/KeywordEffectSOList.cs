using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keyword_Effect_List", menuName = "SO/CardDataSO/Effect/EffectList")]
public class KeywordEffectSOList : ScriptableObject
{
    public List<KeywordEffectSO> keywordEffects = new();
}
