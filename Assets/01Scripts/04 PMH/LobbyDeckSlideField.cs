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
                // ���� ���콺 ��ġ�� �����ͼ� SpinDeckField�� ����
                SpinDeckField(Input.mousePosition);
            }
        }

        private void SpinDeckField(Vector2 currentPos)
        {
            // x ��ǥ ���̸� �̿��� �������� ���������� �Ǻ�
            float direction = currentPos.x - _clickPos.x;
            if (direction > 0)
            {
                Debug.Log("���������� �̵� ��");
                cardDeckHolder.eulerAngles -= new Vector3(0, 0, Time.deltaTime * (spinSpeed * (Mathf.Abs(direction / 250))));
            }
            else if (direction < 0)
            {
                Debug.Log("�������� �̵� ��");
                cardDeckHolder.eulerAngles += new Vector3(0, 0, Time.deltaTime * (spinSpeed * (Mathf.Abs(direction / 250))));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Ŭ��");
            canMoving = true;
            _clickPos = eventData.position; // ó�� Ŭ�� ��ġ ����
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Ŭ�� �ø�");
            canMoving = false; // Ŭ���� �ø��� �̵� ����
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
