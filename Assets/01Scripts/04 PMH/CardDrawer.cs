using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrawer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<CardSO> _deck = new();
    [SerializeField] private RectTransform spawnTrm;
    [SerializeField] private RectTransform cardUseArea;

    public void InitializeDeck(List<CardSO> deck)
    {
        _deck = deck;
        ShuffleDeck();
    }

    private void ShuffleDeck()
    {
        if (_deck.Count == 0 || _deck.Count == 1) return;

        for(int i = 0; i < _deck.Count; i++)
        {
            int temp = Random.Range(0, _deck.Count);
            CardSO tempCard = _deck[temp];
            _deck[temp] = _deck[i];
            _deck[i] = tempCard;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CardSO randomCard = GetCardFromDeck();

        if (randomCard == null) return;

        CardObject cardObj = Instantiate(
            randomCard.cardObject, spawnTrm.localPosition, Quaternion.identity);
        cardObj.transform.SetParent(spawnTrm);

        cardObj.SetCardUseArea(cardUseArea);

        CardManager.Instance.AddToFieldCard(cardObj);
    }

    private CardSO GetCardFromDeck()
    {
        if (_deck.Count == 0) return null;

        CardSO card = _deck[0];
        _deck.RemoveAt(0);
        _deck.Add(card);

        return card;
    }
}
