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
            print(SkillManager.Instance);
            
            SkillManager.Instance.OnSkillRegisted += RegistSkillImage;
            RegistSkillImage();
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

        private void OnDisable()
        {
            SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
        }

        //private void OnDestroy()
        //{
        //    //SkillManager.Instance.OnSkillRegisted -= RegistSkillImage;
        //}
    }
}
