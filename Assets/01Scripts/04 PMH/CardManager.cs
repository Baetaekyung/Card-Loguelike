using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private List<BaseCard> _cardList = new();
    private List<BaseCard> _usedCardList = new();
    private List<BaseCard> _haveCardList = new();
    private Dictionary<CardType, BaseCard> _card;

    [SerializeField] private float _verticalSpacing;
    [SerializeField] private float _minVerticalSpacing = 40f;
    [SerializeField] private Vector2 _usedCardSortingPosition;
    [SerializeField] private Vector2 _usedCardOffset;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);
    }

    private void Update()
    {
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
            //이거 왜 두번 함?
            item.SetSiblingIndex(0);
            item.SetSiblingIndex(offset - 1);
        }
    }

    public void AlignmentCards()
    {
        if (_haveCardList.Count == 0) return;

        //카드가 많아짐에 따라서 점점 간격이 감소하도록
        float offsetX = _verticalSpacing - _haveCardList.Count * 2f;

        //최소한의 간격을 지녀야 구별이 가능함
        offsetX = Mathf.Clamp(offsetX, _minVerticalSpacing, offsetX);

        for (int i = 0; i < _haveCardList.Count; i++)
        {
            float positionX = (i - (_haveCardList.Count - 1) / 2f) * offsetX;
            _haveCardList[i].SetAlignmentPosition(new Vector3(positionX, 0, 0));
        }
    }

    public void AddedToHaveCard(BaseCard addedCard) => _haveCardList.Add(addedCard);

    public void SetUsedCard(BaseCard card) => _usedCardList.Add(card);

    public Vector2 GetUsedCardPosition() => _usedCardSortingPosition;
}
