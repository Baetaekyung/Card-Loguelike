using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class SettingBtn : BaseButton
    {
        [SerializeField] private CanvasGroup _settingPanel;
        [SerializeField] private float _fadeTime;

        protected override void Awake()
        {
            base.Awake();
            _settingPanel.alpha = 0f;
        }

        protected override void OnClick()
        {
            base.OnClick();
            _settingPanel.DOFade(1, _fadeTime);
        }
    }
}
