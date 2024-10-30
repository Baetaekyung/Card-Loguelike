using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SkillCard : BaseCard
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
