using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "KeywordEffect_", menuName = "SO/CardEffect/Keyword_Effect")]
public class KeywordEffectSO : ScriptableObject
{
    public string keyword;

    public UnityEvent effectEvent;

    public void ExcuteEffect()
    {
        effectEvent?.Invoke();
    }
}
