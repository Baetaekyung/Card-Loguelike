public class CardUI : BaseCard
{
    private void OnEnable()
    {
        OnPointerDownEvent += OnPointerDownHandler;
    }

    private void OnPointerDownHandler()
    {
        CardManager cardManager = CardManager.Instance;

        cardManager.deckCardList.Add(cardManager.nameByDictionary[CardInfo.cardName].cardObject);
        
        Destroy(gameObject);
    }

    protected override void UpdatePosition()
    {
        //dont update position
    }

    private void OnDestroy()
    {
        OnPointerDownEvent -= OnPointerDownHandler;
    }
}
