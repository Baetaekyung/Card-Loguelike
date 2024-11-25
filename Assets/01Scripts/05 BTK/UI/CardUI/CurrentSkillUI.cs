using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CurrentSkillUI : MonoSingleton<CurrentSkillUI>
    {
        [SerializeField] private Image[] _skillImages;
        [SerializeField] private Sprite defaultSpirte = default;

        //private void OnEnable()
        //{
        //}
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            //Debug.LogError("커런트스킬유아이스크립트스타트함수");
            SkillManager.Instance.RemoveRegisterSkill();
            SkillManager.Instance.OnSkillRegisted += RegistSkillImage;
            //SkillManager.Instance.OnSkillResetRegisted += RemoveSkillImage;
            //RegistSkillImage();
        }

        private void OnEnable()
        {
            //Debug.LogError("커런트스킬유아이스크립트온이너블함수");
            SkillManager.Instance.OnSkillRegisted += RegistSkillImage;
            GameObject cardContainer = GameObject.Find("CardContainer");
            if (cardContainer != null)
            {
                int n = cardContainer.transform.childCount;
                for (int i = 0; i < n; i++)
                {
                    GameObject card = cardContainer.transform.GetChild(i).gameObject;
                    int k = card.transform.childCount;
                    for(int j = 0; j < k; j++)
                    {
                        if(card.transform.GetChild(j).TryGetComponent(out Image cardimage))
                        {
                            cardimage.enabled = false;
                        }
                    }
                }
            }
        }

        private void RegistSkillImage()
        {
            //Debug.LogError("See you tomorrow");
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
        public void RemoveSkillImage()
        {
            foreach (var item in _skillImages)
            {
                item.sprite = defaultSpirte;
            }
        }
        public void SetRegisterCardImage()
        {
            for (int i = 0; i < SkillManager.Instance.registerSkills.Count; i++)
            {
                print(SkillManager.Instance.registerSkills[i].SkillImage);
                _skillImages[i].sprite = SkillManager.Instance.registerSkills[i].SkillImage;
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
