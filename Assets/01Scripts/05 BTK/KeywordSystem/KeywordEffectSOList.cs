using CardGame;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordEffect_List", menuName = "SO/CardEffect/KeywordEffectList")]
public class KeywordEffectSOList : ScriptableObject
{
    public List<KeywordEffectSO> keywordEffects = new();
}
