using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class DeckSettingImage : MonoBehaviour
        , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] protected float _hoverSize;
        [SerializeField] protected float _hoverAnimationSpeed;
       
        protected bool _isHovering = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Go to deck setting Scene!!");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isHovering)
            {
                _isHovering = true;
                transform.DOScale(Vector3.one * _hoverSize, 1 / _hoverAnimationSpeed)
                    .SetEase(Ease.OutBounce);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isHovering)
            {
                _isHovering = false;
                transform.DOScale(Vector3.one, 1 / _hoverAnimationSpeed)
                    .SetEase(Ease.InBounce);
            }
        }
    }
}
