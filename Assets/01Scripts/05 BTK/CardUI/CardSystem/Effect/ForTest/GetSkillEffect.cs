using UnityEngine;

[CreateAssetMenu(fileName = "GetSkill_", menuName = "SO/CardEffect/GetSkill")]
public class GetSkillEffect : BaseEffect
{
    //[SerializeField] private Skill _skill;
    //For test I use string
    [SerializeField] private string _skillName;

    public override void ExcuteEffect()
    {
        Debug.Log($"Get {_skillName} Skill");
        //PlayerManager.Instance.AddSkill(_skill);
        //이런식으로 짜야 할 거 같은데, 지금 뭐 있는게 없어서 뭐 할 수가 없음;;
    }
}
