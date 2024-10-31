using UnityEngine;

public class SkillCard : BaseCard
{
    [SerializeField] private ParticleSystem _skillTypeEffect;

    protected virtual void OnEnable()
    {
        OnCardUseEvent += SkillExcute;
    }

    protected virtual void SkillExcute()
    {
        Debug.Log("��ų ���");
        _skillTypeEffect.Play();
    }

    protected virtual void OnDestroy()
    {
        OnCardUseEvent -= SkillExcute;
    }
}
