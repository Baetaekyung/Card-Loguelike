using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class BaseCard : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected RectTransform _cardTrm;
    public RectTransform GetTransform() => _cardTrm;

    [Header("UI datas")]
    [SerializeField] protected CardDataSO _cardData;
    public CardDataSO CardData { get => _cardData; set => _cardData = value; }
    [SerializeField] protected TextMeshProUGUI _costText;
    [SerializeField] protected TextMeshProUGUI _cardNameText;
    [SerializeField] protected TextMeshProUGUI _cardDescription;
    [SerializeField] protected Image _cardImage;

    [Header("Hover datas")]
    [SerializeField] protected float _cardHoverSize;
    [SerializeField] protected float _hoverHeight;
    [SerializeField] protected float _hoverAnimationTime = 2f;

    public CardInfo CardInfo => _cardData.cardInfo;

    [Header("Boolean valiables")]
    protected bool _isHovering;

    #region Events

    public Action OnPointerEnterEvent;
    public Action OnPointerExitEvent;
    public Action OnPointerDownEvent;
    public Action OnPointerUpEvent;
    public Action OnCardUseEvent;

    #endregion

    protected virtual void Awake()
    {
        InitializeCard();
        _isHovering = false;
    }

    public virtual void InitializeCard()
    {
        _costText.text = CardInfo.cost.ToString();
        _cardNameText.text = CardInfo.cardName;
        _cardDescription.text = CardInfo.cardDescription;
        _cardImage.sprite = CardInfo.cardSprite;
    }

    protected virtual void Update()
    {
        UpdateSize();
        UpdatePosition();
    }

    protected virtual void UpdateSize()
    {
        if (_isHovering)
        {
            Vector3 targetSize = Vector3.one * _cardHoverSize;

            _cardTrm.localScale = Vector3.Lerp(_cardTrm.localScale, targetSize,
                Time.deltaTime * _hoverAnimationTime);
        }
        else
        {
            _cardTrm.localScale = Vector3.Lerp(_cardTrm.localScale, Vector3.one,
                Time.deltaTime * _hoverAnimationTime);
        }
    }
    protected virtual void UpdatePosition()
    {
        if (_isHovering)
        {
            Vector3 targetPos = _cardTrm.localPosition + Vector3.up * _hoverHeight;
            _cardTrm.localPosition = Vector3.Lerp(
                                    _cardTrm.localPosition,
                                    targetPos,
                                    Time.deltaTime * _hoverAnimationTime);
            transform.SetAsLastSibling();
            return;
        }
    }

    public int GetSiblingIndex() => _cardTrm.GetSiblingIndex();

    public void SetSiblingIndex(int idx) => _cardTrm.SetSiblingIndex(idx);

    public RectTransform GetRectTransform() => _cardTrm;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("�������ʹٿ�");
        if (_isHovering)
        {
            OnPointerDownEvent?.Invoke();
            _isHovering = false;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (_isHovering)
        {
            _isHovering = false;
        }
        OnPointerUpEvent?.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHovering)
        {
            _isHovering = true;
            OnPointerEnterEvent?.Invoke();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (_isHovering)
        {
            _isHovering = false;
            OnPointerExitEvent?.Invoke();
        }
    }
}
