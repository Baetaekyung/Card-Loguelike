using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseCard : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Transform
    protected RectTransform _cardTrm;
    #region Position property

    public RectTransform GetTransform() => _cardTrm;

    #endregion
    #endregion

    #region UI datas

    [Header("UI datas")]
    [SerializeField] protected CardDataSO _cardData;
    [SerializeField] protected TextMeshProUGUI _costText;
    [SerializeField] protected TextMeshProUGUI _cardNameText;
    [SerializeField] protected TextMeshProUGUI _cardDescription; //ī�� ����
    [SerializeField] protected Image _cardImage;

    [SerializeField] protected RectTransform _cardUseArea;
    private float _cardUseHeight;

    public CardDataSO CardData
    {
        get => _cardData;
        set => _cardData = value;
    }

    #endregion

    #region Hover datas

    [Header("Hover datas")]
    [SerializeField] protected float _cardHoverSize;
    [SerializeField] protected float _hoverHeight; //���콺 ���ٴ�� ���� ��¦ �ö����
    [SerializeField] protected float _hoverAnimationTime = 2f; //�ִϸ��̼� ��¦ �ֱ�
    protected Vector3 _alignmentPosition;
    protected int _hirachyIndex;

    #endregion

    #region UsedCard Data

    [SerializeField] private float _usedCardAimationSpeed = 3f; //���� ������ ���� �ӵ�

    #endregion

    protected CardInfo _cardInfo => _cardData.cardInfo;

    #region State boolean

    [Header("Boolean valiables")]
    protected bool _isHovering;
    protected bool _isDragging;
    protected bool _isUsed;

    #endregion

    #region Events

    public event Action OnPointerEnterEvent;
    public event Action OnPointerExitEvent;
    public event Action OnPointerDownEvent;
    public event Action OnPointerUpEvent;
    public event Action OnCardUseEvent;

    #endregion

    private bool _isGoaled;

    protected virtual void Awake()
    {
        _cardTrm = GetComponent<RectTransform>();
        InitializeCard();
    }

    private void Start()
    {
        _cardUseHeight = _cardUseArea.rect.height;

        _isDragging = false;
        _isHovering = false;
        _isUsed = false;

        _alignmentPosition = Vector3.zero;
        _hirachyIndex = transform.GetSiblingIndex();
    }

    public virtual void InitializeCard()
    {
        _costText.text = _cardInfo.cost.ToString();
        _cardNameText.text = _cardInfo.cardName;
        _cardDescription.text = _cardInfo.cardDescription;
        _cardImage.sprite = _cardInfo.cardSprite;
    }

    protected virtual void Update()
    {
        if (_isUsed) return;
        UpdatePosition();
        UpdateSize();
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
        if (_isDragging)
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos = new Vector2(pos.x * Screen.width, pos.y * Screen.height);
            _cardTrm.position = pos;
            return;
        }
        if (_isHovering)
        {
            Vector3 targetPos = _alignmentPosition + Vector3.up * _hoverHeight;
            _cardTrm.localPosition = Vector3.Lerp(
                                    _cardTrm.localPosition,
                                    targetPos,
                                    Time.deltaTime * _hoverAnimationTime);
            return;
        }

        _cardTrm.localPosition =     Vector3.Lerp(
                                    _cardTrm.localPosition,
                                    _alignmentPosition,
                                    Time.deltaTime * _hoverAnimationTime);
    }

    public void UsedVisualizing(Vector2 offsetPos, int offset)
    {
        Vector2 pos = CardManager.Instance.GetUsedCardPosition() + (offsetPos * offset);

        if(_isGoaled == false) MoveToUsedDeck(_cardTrm, pos);

        _cardTrm.localScale = Vector3.one;
    }

    private void MoveToUsedDeck(RectTransform cardTrm, Vector2 targetPos)
        => StartCoroutine(MoveToUsedDeckRoutine(cardTrm, targetPos));

    private IEnumerator MoveToUsedDeckRoutine(RectTransform cardTrm, Vector2 targetPos)
    {
        _isGoaled = true;

        Vector2 rectDir = cardTrm.anchoredPosition;
        WaitForSeconds wfs = new WaitForSeconds(Time.deltaTime * _usedCardAimationSpeed);

        float sequenceTime = 0;
        while(sequenceTime < 0.99f)
        {
            sequenceTime += Time.deltaTime * _usedCardAimationSpeed;
            cardTrm.anchoredPosition = Vector2.Lerp(rectDir, targetPos, sequenceTime);
            cardTrm.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, 360), sequenceTime);
            yield return wfs;
        }
        cardTrm.anchoredPosition = targetPos;
        cardTrm.eulerAngles = new Vector3(0, 0, 0);
    }

    public int GetSiblingIndex() => _cardTrm.GetSiblingIndex();

    public CardDataSO GetCardData() => _cardData;

    public void SetSiblingIndex(int idx) => _cardTrm.SetSiblingIndex(idx);

    public RectTransform GetRectTransform() => _cardTrm;

    public void SetAlignmentPosition(Vector3 target) => _alignmentPosition = target;

    public void SetCardUseArea(RectTransform cardUseArea) => _cardUseArea = cardUseArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isUsed) return; //�̹� ���������� ���� �ȵǰ�

        if (_isHovering)
        {
            _isDragging = true;
            _isHovering = false;

            OnPointerDownEvent?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isUsed) return;

        if (_isDragging)
        {
            _isDragging = false;
            _isHovering = false;
            OnPointerUpEvent?.Invoke();
        }

        if (_cardTrm.position.y > _cardUseHeight)
        {
            //ī����
            _isUsed = true;
            OnCardUseEvent?.Invoke();

            if(_isUsed) CardManager.Instance.SetUsedCard(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHovering)
        {
            _isHovering = true;
            transform.SetAsLastSibling();
            OnPointerEnterEvent?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isDragging) return;

        if (_isHovering)
        {
            transform.SetSiblingIndex(_hirachyIndex);
            OnPointerExitEvent?.Invoke();
            _isHovering = false;
        }
    }
}
