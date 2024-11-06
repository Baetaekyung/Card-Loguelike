using UnityEngine;

public class KeywordEffects : MonoBehaviour
{
    public static KeywordEffects Instance;
    public KeywordEffectSOList effectList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(Instance.gameObject);
    }
}
