using CardGame;
using UnityEngine;

public class StartGameBtn : BaseButton
{
    [SerializeField] private FadePanel _fadePanel;
    [SerializeField] private string _nextSceneName;

    protected override void OnClick()
    {
        base.OnClick();
        _fadePanel.FadeIn(_nextSceneName);
    }
}
