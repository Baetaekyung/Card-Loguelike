using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<BaseCard> cardList;
    public List<BaseCard> useCardList;
    public List<BaseCard> haveCardList;
    public Dictionary<CardType, BaseCard> _card;

    public Vector2 usedCardSortingPosition;
    [SerializeField] private Vector2 usedCardOffset;

    public static CardManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);

        BaseCard[] baseCards = FindObjectsByType<BaseCard>(FindObjectsSortMode.None);
        foreach(var item in baseCards)
        {
            cardList.Add(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SortingUsedCards();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SortingCardTosBucheggol(haveCardList);
        }
    }
    public void SetCard(BaseCard card)
    {
        Debug.Log(card.name + " Card´Ô ¸®½ºÆ®¿¡ ÀÔ°¶");
        useCardList.Add(card);
    }

    private void SortingUsedCards()
    {
        int offset = 0;
        foreach(var item in useCardList)
        {
            offset++;
            item.UsedVisualizing(usedCardOffset, offset);
            item.SetSibana(0);
            item.SetSibana(offset - 1);
        }
    }

    public void AddedForHaveCardList(BaseCard addedCard)
    {
        haveCardList.Add(addedCard);
    }

    public void SortingCardTosBucheggol(List<BaseCard> cards)
    {
        
        int idx = 0;
        foreach (var item in cards)
        {
            float y;
            y = item.GetRect().anchoredPosition.y;
            Vector3 target = new Vector3(1 - Mathf.Pow(idx - (cards.Count - 1) / 2f, 2) / Mathf.Pow((cards.Count - 1) / 2f, 2), y, 0);
            item.SetOrginePosition(target);
            idx++;
            
        }
    }

}
