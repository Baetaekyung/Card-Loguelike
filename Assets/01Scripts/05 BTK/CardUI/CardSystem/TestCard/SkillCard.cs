using UnityEngine;

public class SkillCard : CardObject
{
    protected virtual void OnEnable()
    {
        OnCardUseEvent += SkillExcute;
    }

    protected virtual void SkillExcute()
    {
        Debug.Log("스킬 사용");
    }

    protected virtual void OnDestroy()
    {
        OnCardUseEvent -= SkillExcute;
    }
}
