using System.Collections.Generic;
using UnityEngine;

public class CardHorizontalViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnTrm;
    [SerializeField] private float _verticalSpacing;
    private List<CardUI> _selectableCards = new();

    private void Start()
    {
        SetCardToViewer();
    }

    private void SetCardToViewer()
    {
        for (int i = 0; i < CardManager.Instance.haveCards.Count; i++)
        {
            CardUI cardUI = Instantiate(CardManager.Instance.haveCards[i].cardUI, _spawnTrm, false);
            cardUI.InitializeCard();
            _selectableCards.Add(cardUI);
        }
    }
}
