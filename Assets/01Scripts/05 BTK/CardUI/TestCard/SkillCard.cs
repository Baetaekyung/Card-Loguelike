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
        Debug.Log("��ų ���");
    }

    protected virtual void OnDestroy()
    {
        OnCardUseEvent -= SkillExcute;
    }
}
