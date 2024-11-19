using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class DeckSettingImage : MonoBehaviour
        , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private float _hoverSize;
        [SerializeField] private float _hoverAnimationSpeed;
        [SerializeField] private FadePanel _fadePanel;
        [SerializeField] private string _nextSceneName;

        private bool _isHovering = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _fadePanel.FadeIn(_nextSceneName);
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
