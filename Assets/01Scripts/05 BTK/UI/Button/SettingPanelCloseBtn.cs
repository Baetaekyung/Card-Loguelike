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
            PopUpManager.Instance.SetSettingPanelFalse();
        }
    }
}
