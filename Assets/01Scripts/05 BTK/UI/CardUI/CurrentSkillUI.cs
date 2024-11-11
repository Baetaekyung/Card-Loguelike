using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace CardGame
{
    public class CurrentSkillUI : MonoBehaviour
    {
        [SerializeField] private Image[] _skillImages;

        private void OnEnable()
        {
            SkillManager.Instance.OnSkillRegisted += RegistSkillImage;
        }

        private void RegistSkillImage()
        {
            for (int i = 0; i < SkillManager.Instance.registerSkills.Count; i++)
            {
                _skillImages[i].sprite = SkillManager.Instance.registerSkills[i].SkillImage;
            }
        }

        private void OnDisable()
        {
            SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
        }

        private void OnDestroy()
        {
            SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
        }
    }
}
