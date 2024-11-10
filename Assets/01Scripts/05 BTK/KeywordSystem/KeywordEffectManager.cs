using CardGame;
using System.Collections.Generic;
using UnityEngine;

public class KeywordEffectManager : MonoSingleton<KeywordEffectManager>
{
    public KeywordEffectSOList effectList;
    public Dictionary<KeywordEnum, int> keywordCountDictionary = new();
}
