using UnityEngine;

[CreateAssetMenu(fileName = "GetSkill_", menuName = "SO/CardDataSO/Effect/GetSkill")]
public class GetSkillEffect : BaseEffect
{
    //[SerializeField] private Skill _skill;
    //For Test I use string 
    [SerializeField] private string _skillName;

    public override void ExcuteEffect()
    {
        Debug.Log($"Get {_skillName} Skill");
        //PlayerManager.Instance.AddSkill(_skill);
        //�̷������� ¥�� �� �� ������, ���� �� �ִ°� ��� �� �� ���� ����;;
    }
}
