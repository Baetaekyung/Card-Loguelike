using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class SettingPanelCloseBtn : BaseButton
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        protected override void OnClick()
        {
            base.OnClick();
            _canvasGroup.DOFade(0, 0.2f);
        }
    }
}
