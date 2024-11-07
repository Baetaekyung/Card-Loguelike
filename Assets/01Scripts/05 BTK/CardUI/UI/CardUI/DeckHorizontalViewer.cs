using System.Collections.Generic;
using UnityEngine;

public class DeckHorizontalViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnTrm;
    [SerializeField] private CardHorizontalViewer _cardViewer;
    private CardManager cardManager => CardManager.Instance;

    private List<CardUI> _cards = new List<CardUI>();

    private int _onDeckCardCnt = 0;

    private void Update()
    {
        if(cardManager.deckCnt != _onDeckCardCnt)
        {
            SetMyDeck();
        }
    }

    public void SetMyDeck()
    {
        if (_cards.Count != 0)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                Destroy(_cards[i].gameObject);
            }
        }

        _cards.Clear();

        for (int i = 0; i < cardManager.deckCnt; i++)
        {
            CardUI cardUI = Instantiate(cardManager.haveCards[i].cardUI, _spawnTrm);
            cardUI.InitializeCard();
            cardUI.isOnDeck = true;
            _cards.Add(cardUI);
            _onDeckCardCnt++;
        }
        _onDeckCardCnt = cardManager.deckCnt;
    }

    public void ResetDeck()
    {
        if (_cards.Count == 0) return;

        for (int i = 0; i < _cards.Count; i++)
        {
            Destroy(_cards[i].gameObject);
        }

        cardManager.deckCnt -= _cards.Count;
        _cards.Clear();
    }
}
