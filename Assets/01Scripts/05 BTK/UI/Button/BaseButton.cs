using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
    , IPointerEnterHandler, IPointerExitHandler
{
    protected Button _button;

    [SerializeField] protected float _hoverSize;
    [SerializeField] protected float _hoverAnimationSpeed;
    protected bool _isHovering = false;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isHovering)
        {
            _isHovering = true;
            transform.DOScale(Vector3.one * _hoverSize, 1 / _hoverAnimationSpeed)
                .SetEase(Ease.OutBounce);
        }
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if(_isHovering)
        {
            _isHovering = false;
            transform.DOScale(Vector3.one, 1 / _hoverAnimationSpeed)
                .SetEase(Ease.InBounce);
        }
    }
}
