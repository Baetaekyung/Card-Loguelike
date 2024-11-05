using System.Collections.Generic;
using UnityEngine;

public class CardHorizontalViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnTrm;
    [SerializeField] private DeckHorizontalViewer _deckHorizontalViewer;

    private CardManager cardManager => CardManager.Instance;

    private List<CardUI> _cardUIs = new List<CardUI>();

    private int _onDeckCardCnt = 0;

    private void Update()
    {
        if(cardManager.deckCnt != _onDeckCardCnt)
        {
            SetCardToViewer();
        }
    }

    private void SetCardToViewer()
    {
        if(_cardUIs.Count != 0)
        {
            for(int i = 0; i < _cardUIs.Count; i++)
            {
                Destroy(_cardUIs[i].gameObject);
            }
        }

        _cardUIs.Clear();

        for(int i = 0; i < cardManager.haveCards.Count; i++)
        {
            if (!cardManager.haveCards[i].cardUI.isOnDeck)
            {
                CardUI cardUI = Instantiate(cardManager.haveCards[i].cardUI, _spawnTrm);
                cardUI.InitializeCard();
                _cardUIs.Add(cardUI);
            }
            else
            {
                _onDeckCardCnt++;
            }
        }
    }
}
