using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseCard : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Transform
    protected RectTransform _card;
    #region Position property

    public RectTransform GetTransform() => _card;

    #endregion
    #endregion

    #region UI datas

    [Header("UI datas")]
    [SerializeField] protected CardDataSO _cardData;
    [SerializeField] protected TextMeshProUGUI _costText;
    [SerializeField] protected TextMeshProUGUI _cardNameText;
    [SerializeField] protected TextMeshProUGUI _cardEffectDescription; //카드효과 설명
    [SerializeField] protected TextMeshProUGUI _cardDescription; //카드 설명
    [SerializeField] protected Image _cardImage;
    [SerializeField] protected RectTransform _cardUseArea;
    protected float _cardUseHeight;
    
    #endregion

    #region Hover datas

    [Header("Hover datas")]
    [SerializeField] protected float _cardHoverSize;
    [SerializeField] protected float _hoverHeight; //마우스 갖다대면 위로 살짝 올라오게
    [SerializeField] protected float _hoverAnimationTime = 2f; //애니메이션 살짝 주기
    protected Vector3 _originPosition; //처음 카드가 위치한 포지션

    #endregion

    protected CardInfo _cardInfo;

    #region State boolean

    [Header("Boolean valiables")]
    protected bool _isHovering;
    protected bool _isDragging;

    #endregion

    //이거 인터페이스 오버라이딩 못해서 여기에 등록해서 사용
    #region Events

    public event Action OnPointerEnterEvent;
    public event Action OnPointerExitEvent;
    public event Action OnPointerDownEvent;
    public event Action OnPointerUpEvent;
    public event Action OnCardUseEvent;

    #endregion

    protected virtual void Awake()
    {
        _card = GetComponent<RectTransform>();
        _cardInfo = _cardData.cardInfo;
        _cardUseHeight = _cardUseArea.rect.height;
        _originPosition = _card.localPosition; //처음 위치
        Debug.Log(_cardUseHeight);
        InitializeCard();
    }

    protected virtual void InitializeCard()
    {
        _costText.text = _cardInfo.cost.ToString();
        _cardNameText.text = _cardInfo.cardName;
        _cardEffectDescription.text = _cardInfo.cardEffectDescription;
        _cardDescription.text = _cardInfo.cardDescription;
        _cardImage.sprite = _cardInfo.cardSprite;
    }

    protected virtual void Update()
    {
        UpdatePosition();
        UpdateSize();
    }

    protected virtual void UpdateSize()
    {
        if (_isHovering)
        {
            Vector3 targetSize = Vector3.one * _cardHoverSize;

            _card.localScale = Vector3.Lerp(_card.localScale, targetSize,
                Time.deltaTime * _hoverAnimationTime);
        }
        else
        {
            _card.localScale = Vector3.Lerp(_card.localScale, Vector3.one,
                Time.deltaTime * _hoverAnimationTime);
        }
    }

    protected virtual void UpdatePosition()
    {
        if (_isDragging)
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos = new Vector2(pos.x * Screen.width, pos.y * Screen.height);
            _card.position = pos;
            return;
        }
        if (_isHovering)
        {
            Vector3 targetPos = _originPosition + Vector3.up * _hoverHeight;
            _card.localPosition = Vector3.Lerp(
                                    _card.localPosition,
                                    targetPos,
                                    Time.deltaTime * _hoverAnimationTime);
            return;
        }

        _card.localPosition = Vector3.Lerp(
                                    _card.localPosition,
                                    _originPosition,
                                    Time.deltaTime * _hoverAnimationTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isHovering)
        {
            _isDragging = true;
            _isHovering = false;

            OnPointerDownEvent?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDragging)
        {
            _isDragging = false;
            _isHovering = false;
            OnPointerUpEvent?.Invoke();
        }

        if (_card.position.y > _cardUseHeight)
        {
            Debug.Log("Card 사용");
            OnCardUseEvent?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHovering)
        {
            _isHovering = true;
            OnPointerEnterEvent?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isDragging) return;

        if (_isHovering)
        {
            OnPointerExitEvent?.Invoke();
            _isHovering = false;
        }
    }
}
