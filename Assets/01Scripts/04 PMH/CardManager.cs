using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public CardDrawer cardDrawer;

    public List<CardSO> haveCards = new();          //player have cards
    public List<CardSO> deckCardList = new();     //chosen deck
    public int deckCnt = 0;
    public List<CardObject> fieldCardList = new();  //card to alignment
    private List<CardObject> _usedCardList = new(); //used card
    public Dictionary<string, CardSO> nameByDictionary = new();

    private List<string> _cardNameList = new(); //For data Save

    [SerializeField] private float _verticalSpacing; //Card vertical space
    [SerializeField] private float _minVerticalSpacing = 40f;
    [SerializeField] private Vector2 _usedCardSortingPosition; //used card on this position
    [SerializeField] private Vector2 _usedCardOffset; //used card need to offset for pile effect

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);
    }

    private void Start()
    {
        CardDataManager.Instance.LoadHavingCard();

        for (int i = 0; i < haveCards.Count; i++)
        {
            if(!nameByDictionary.ContainsKey(haveCards[i].cardUI.CardInfo.cardName))
                nameByDictionary.Add(haveCards[i].cardUI.CardInfo.cardName, haveCards[i]);

            _cardNameList.Add(haveCards[i].cardUI.CardInfo.cardName);
        }

        //After Init nameByDictionary
        CardDataManager.Instance.LoadCurrentDeck();

        cardDrawer.InitializeDeck(deckCardList);

        deckCnt = deckCardList.Count;
    }

    private void Update()
    {
        //Debug
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            CardDataManager.Instance.SaveCurrentDeck(_cardNameList);
            return;
        }

        AlignmentCards();
        SortingUsedCards();
    }

    private void SortingUsedCards()
    {
        if (_usedCardList.Count == 0) return;

        int offset = 0;
        foreach (var item in _usedCardList)
        {
            offset++;
            item.UsedVisualizing(_usedCardOffset, offset);
            //why it do twice?
            item.SetSiblingIndex(0);
            item.SetSiblingIndex(offset - 1);
        }
    }

    public void AlignmentCards()
    {
        if (fieldCardList.Count == 0) return;

        //if card increase spacing will be decrease
        float offsetX = _verticalSpacing - fieldCardList.Count * 2f;

        //to separate what card
        offsetX = Mathf.Clamp(offsetX, _minVerticalSpacing, offsetX);

        for(int i = 0; i < fieldCardList.Count; i++)
        {
            if (fieldCardList[i].IsUsed)
            {
                fieldCardList.RemoveAt(i);
                return;
            }
            float positionX = (i - (fieldCardList.Count - 1) / 2f) * offsetX;
            fieldCardList[i].SetAlignmentPosition(new Vector3(positionX, 0, 0));
            fieldCardList[i].SetHirachyIndex(i);
        }
    }

    public void AddToHaveCard(CardSO cardToAdd) => haveCards.Add(cardToAdd);

    public void AddToFieldCard(BaseCard cardToAdd) => fieldCardList.Add(cardToAdd as CardObject);

    public void ClearToFieldCard() => fieldCardList.Clear();

    public void AddToDeckCard(CardSO cardToAdd) => deckCardList.Add(cardToAdd);

    public void SetUsedCard(BaseCard card) => _usedCardList.Add(card as CardObject);

    public Vector2 GetUsedCardPosition() => _usedCardSortingPosition;
}
