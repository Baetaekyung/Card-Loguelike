using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TakeOutCard : MonoBehaviour, IPointerDownHandler
{
    public List<BaseCard> cards;
    public RectTransform spawnTrm;
    public RectTransform cardUseArea;
    private void Awake()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        BaseCard randomCard = TakeRandomCard();
        CardManager.Instance.AddedForHaveCardList(randomCard);
        GameObject randomCardMing = Instantiate(randomCard.gameObject, spawnTrm.anchoredPosition, Quaternion.identity);
        randomCardMing.gameObject.transform.parent = transform.parent;
        randomCardMing.GetComponent<BaseCard>()._cardUseArea = cardUseArea;
    }

    private BaseCard TakeRandomCard()
    {
        int cardLength = 0;
        foreach(var card in cards)
        {
            cardLength++;
        }

        int rand = UnityEngine.Random.Range(0, cardLength);

        return cards[rand];
    }
}
