using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Keyword_Effect_", menuName = "SO/CardDataSO/Effect/Keyword_Effect")]
public class KeywordEffectSO : ScriptableObject
{
    public string keyword;

    public UnityEvent effectEvent;

    public void ExcuteEffect()
    {
        effectEvent?.Invoke();
    }
}
