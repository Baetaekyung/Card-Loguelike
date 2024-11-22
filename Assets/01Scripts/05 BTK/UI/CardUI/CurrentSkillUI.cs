using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CurrentSkillUI : MonoBehaviour
    {
        [SerializeField] private Image[] _skillImages;
        [SerializeField] private Sprite defaultSpirte;

        //private void OnEnable()
        //{
        //}
        private void Start()
        {
            SkillManager.Instance.RemoveRegisterSkill();
            SkillManager.Instance.OnSkillRegisted += RegistSkillImage;
            SkillManager.Instance.OnSkillResetRegisted += RemoveSkillImage;
            //RegistSkillImage();
        }

        private void RegistSkillImage()
        {
            //print(_skillImages.Length);
            foreach (var item in _skillImages)
            {
                item.sprite = defaultSpirte;
            }
            print(SkillManager.Instance.registerSkills.Count);
            for (int i = 0; i < SkillManager.Instance.registerSkills.Count; i++)
            {
                print(SkillManager.Instance.registerSkills[i].SkillImage);
                _skillImages[i].sprite = SkillManager.Instance.registerSkills[i].SkillImage;
            }
        }
        private void RemoveSkillImage()
        {
            foreach (var item in _skillImages)
            {
                item.sprite = defaultSpirte;
            }
        }
        private void OnDisable()
        {
            if(SkillManager.Instance != null)
            {
                SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
            }
        }
        private void OnDestroy()
        {
            if (SkillManager.Instance != null)
            {
                SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
            }
        }

        //private void OnDestroy()
        //{
        //    //SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
        //}
    }
}
