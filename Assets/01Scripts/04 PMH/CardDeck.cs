using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDeck : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<BaseCard> cardPrefabs = new();
    [SerializeField] private RectTransform spawnTrm;
    [SerializeField] private RectTransform cardUseArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        BaseCard randomCard = GetRandomCardFromDeck();

        BaseCard cardObj = Instantiate(
            randomCard, spawnTrm.localPosition, Quaternion.identity);
        cardObj.transform.SetParent(spawnTrm);

        cardObj.SetCardUseArea(cardUseArea);

        CardManager.Instance.AddedToHaveCard(cardObj);
    }

    private BaseCard GetRandomCardFromDeck() => cardPrefabs[Random.Range(0, cardPrefabs.Count)];
}
