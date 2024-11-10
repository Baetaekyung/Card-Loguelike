using CardGame;
using UnityEngine;

[CreateAssetMenu(fileName = "GetSkill_", menuName = "SO/CardEffect/GetSkill")]
public class GetSkillEffect : BaseEffect
{
    [SerializeField] private BaseSkill _skill;

    public override void ExcuteEffect()
    {
        SkillManager.Instance.RegistSkill(_skill);
    }
}
