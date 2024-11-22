using CardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardDrawer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<CardSO> _deck = new();
    [SerializeField] private List<CardObject> _visualCard = new();
    [SerializeField] private RectTransform spawnTrm;
    [SerializeField] private RectTransform cardUseArea;
    private RectTransform _drawer;

    private void OnEnable()
    {
        Debug.Log("밍하하하");

        if (CardManager.Instance.fieldCardList.Count > 0)
        {
            Debug.Log("애안대 애안ㄷ내이기");
            CurrentSkillUI.Instance.RemoveSkillImage();
            SkillManager.Instance.RemoveRegisterSkill();
        }

        AutoDrawCards();
    }

    public void InitializeDeck(List<CardSO> deck)
    {
        _deck = deck;
        _drawer = GetComponent<RectTransform>();
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
        DrawCard();
    }

    private void ResetComplete()
    {
        _deck.Clear();
        CardManager.Instance.ClearToFieldCard();
    }
    private void DrawCard()
    {
        if (SkillManager.Instance.CheckRegisterSkillsCoundIsFull()) return;

        CardSO randomCard = GetCardFromDeck();

        if (randomCard == null) return;

        CardObject cardObj = Instantiate(
            randomCard.cardObject, spawnTrm);
        _visualCard.Add(cardObj);
        cardObj.transform.SetLocalPositionAndRotation(_drawer.localPosition, Quaternion.identity);
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

    private void AutoDrawCards()
    {
        StartCoroutine(AutoDrawCardsCoroutine());
    }
    private IEnumerator AutoDrawCardsCoroutine()
    {
        int a = CardManager.Instance.fieldCardList.Count;
        for (int i = 0; i <= 6 - a; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(3.5f);

        //SceneManager.LoadScene("Map_1 1"); //일단 비워놓음
    }
}
