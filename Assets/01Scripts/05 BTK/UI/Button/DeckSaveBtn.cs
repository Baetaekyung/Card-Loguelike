public class DeckSaveBtn : BaseButton
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnClick()
    {
        //CardDataManager.Instance.SaveCurrentDeck(CardManager.Instance.deckCardList);
    }
}
