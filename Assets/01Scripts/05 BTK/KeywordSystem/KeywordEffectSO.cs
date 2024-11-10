using CardGame;
using UnityEngine;

public class KeywordEffectSO : ScriptableObject
{
    public KeywordEnum keyword;

    protected virtual void Effect(int keywordCount)
    {

    }

    public void ExcuteEffect(int keywordCount)
    {
        Effect(keywordCount);
    }
}
