using UnityEngine;

public class DeckResetBtn : BaseButton
{
    [SerializeField] private DeckHorizontalViewer _deckViewer;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnClick()
    {
        _deckViewer.ResetDeck();
    }
}
