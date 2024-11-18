using CardGame;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardObject : BaseCard
{
    [SerializeField] protected RectTransform _cardUseArea;
    private float _cardUseHeight;
    protected Vector3 _alignmentPosition;
    protected int _hirachyIndex;

    [SerializeField] private float _usedCardAnimationSpeed = 3f; //speed of go to used deck

    [SerializeField] private KeywordPanel _keywordPanel;

    protected bool _isDragging;
    protected bool _isUsed;
    protected bool _isGoaled;
    public bool IsUsed => _isUsed;

    protected void Start()
    {
        _cardUseHeight = _cardUseArea.rect.height;

        _isDragging = false;
        _isHovering = false;
        _isUsed = false;

        _alignmentPosition = Vector3.zero;
    }

    protected override void Update()
    {
        if (IsUsed) return;

        UpdatePosition();
        UpdateSize();
    }

    protected override void UpdatePosition()
    {
        if (_isDragging)
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos = new Vector2(pos.x * Screen.width, pos.y * Screen.height);
            _cardTrm.SetAsLastSibling();
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
            transform.SetAsLastSibling();
            return;
        }

        if(!_isHovering && !_isDragging)
        {
            _cardTrm.localPosition = Vector3.Lerp(
                                    _cardTrm.localPosition,
                                    _alignmentPosition,
                                    Time.deltaTime * _hoverAnimationTime);

            _cardTrm.SetSiblingIndex(_hirachyIndex);
        }
    }

    public void UsedVisualizing(Vector2 offsetPos, int offset)
    {
        Vector2 pos = CardManager.Instance.GetUsedCardPosition() + (offsetPos * offset);

        if (_isGoaled == false) MoveToUsedDeck();

        _cardTrm.localScale = Vector3.one;
    }

    private void MoveToUsedDeck()
    {
        _isGoaled = true;
        Sequence seq = DOTween.Sequence();

        seq.Join(transform.DOLocalMove(
            CardManager.Instance.GetUsedCardPosition(), 1 / _usedCardAnimationSpeed))
            .Join(transform.DORotate(
                new Vector3(0, 0, 360), 1 / _usedCardAnimationSpeed, RotateMode.FastBeyond360));
        seq.Play();
    }

    public void SetAlignmentPosition(Vector3 target) => _alignmentPosition = target;
    public void SetCardUseArea(RectTransform cardUseArea) => _cardUseArea = cardUseArea;
    public void SetHirachyIndex(int idx) => _hirachyIndex = idx;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_isUsed) return; //이미 버려졌으면 실행 안되게

        if (_isHovering)
        {
            _isDragging = true;
            _isHovering = false;
            OnPointerDownEvent?.Invoke();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
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
            //카드사용
            _isUsed = true;
            OnCardUseEvent?.Invoke();

            if (_isUsed) CardManager.Instance.SetUsedCard(this);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHovering)
        {
            _isHovering = true;
            OnPointerEnterEvent?.Invoke();
            _cardTrm.SetAsLastSibling();
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isDragging) return;

        if (_isHovering)
        {
            OnPointerExitEvent?.Invoke();
            _cardTrm.SetSiblingIndex(_hirachyIndex);
            _isHovering = false;
        }
    }
}
