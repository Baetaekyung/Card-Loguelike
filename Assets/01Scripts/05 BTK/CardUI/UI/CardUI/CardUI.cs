public class CardUI : BaseCard
{
    public bool isOnDeck = false;

    private void OnEnable()
    {
        OnPointerDownEvent += OnPointerDownHandler;
        isOnDeck = false;
    }

    private void OnPointerDownHandler()
    {
        CardManager cardManager = CardManager.Instance;

        isOnDeck = !isOnDeck;

        //if(isOnDeck)
            //cardManager.deckCnt++;
        //else
            //cardManager.deckCnt--;
    }

    protected override void UpdatePosition()
    {
        base.UpdatePosition();
    }

    private void OnDestroy()
    {
        OnPointerDownEvent -= OnPointerDownHandler;
    }
}
