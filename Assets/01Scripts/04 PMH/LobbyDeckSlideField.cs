using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class LobbyDeckSlideField : MonoBehaviour
        , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private bool canMoving;
        [SerializeField] private Vector2 _clickPos;

        [SerializeField] RectTransform cardDeckHolder;
        [SerializeField] private float spinSpeed;

        void Update()
        {
            if (canMoving)
            {
                // 현재 마우스 위치를 가져와서 SpinDeckField로 전달
                SpinDeckField(Input.mousePosition);
            }
        }

        private void SpinDeckField(Vector2 currentPos)
        {
            // x 좌표 차이를 이용해 왼쪽인지 오른쪽인지 판별
            float direction = currentPos.x - _clickPos.x;
            if (direction > 0)
            {
                Debug.Log("오른쪽으로 이동 중");
                cardDeckHolder.eulerAngles -= new Vector3(0, 0, Time.deltaTime * (spinSpeed * (Mathf.Abs(direction / 250))));
            }
            else if (direction < 0)
            {
                Debug.Log("왼쪽으로 이동 중");
                cardDeckHolder.eulerAngles += new Vector3(0, 0, Time.deltaTime * (spinSpeed * (Mathf.Abs(direction / 250))));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("클릭");
            canMoving = true;
            _clickPos = eventData.position; // 처음 클릭 위치 저장
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("클릭 올림");
            canMoving = false; // 클릭을 올리면 이동 멈춤
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}
