
using CardGame;
using System.Linq.Expressions;
using UnityEngine;

public class CardUI : BaseCard
{
    public bool isOnDeck = false;
    public int cardCnt;
    public bool IsInDeckCards = false;
    private bool isHold = false;
    public int childIdx = -1;
    private void OnEnable()
    {
        OnPointerDownEvent += OnPointerDownHandler;
        OnPointerUpEvent += OnPointerUpHandler;
        isOnDeck = false;
    }

    private void OnPointerDownHandler()
    {
        isHold = true;

        bool canAddCard = LobbyDeckCardManager.Instance.IsArentExceed(this);

        if (canAddCard && IsInDeckCards == false) //카드를 추가할 수 있고 카드가 아직 덱에 들어가지 않은상태
        {
            Debug.Log("에하하하하(에드카드투덱카드리스트라는뜻");
            IsInDeckCards = true;
            LobbyDeckCardManager.Instance.AddCardToDeckCardList(this);
        }
        else if(IsInDeckCards == true) //카드가 덱에 있는데 한 번 더 눌릴때
        {
            Debug.Log("리하하하하(리무브카드오브덱카드리스트라는뜻");
            LobbyDeckCardManager.Instance.RemoveCardOfDeckCardList(this);
            IsInDeckCards = false;
        }
    }

    private void OnPointerUpHandler()
    {
        isHold = false;
    }

    private void OnDestroy()
    {
        OnPointerDownEvent -= OnPointerDownHandler;
        OnPointerUpEvent -= OnPointerUpHandler;
    }
}
