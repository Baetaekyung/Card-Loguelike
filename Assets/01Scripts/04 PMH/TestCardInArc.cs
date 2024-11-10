using UnityEngine;

namespace CardGame
{
    public class TestCardInArc : MonoBehaviour
    {
        public float radius = 5f;               // 호의 반지름
        public float angle = 90f;               // 전체 호의 각도 (0~360)
        public int segments = 50;               // 호를 구성할 점의 개수
        public Transform[] cards;               // 배치할 카드들의 Transform 배열

        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            DrawArc();

            // 각 카드를 호에 배치
            for (int i = 0; i < cards.Length; i++)
            {
                float normalizedPosition = (float)i / (cards.Length - 1);  // 0~1 사이의 값으로 카드 위치 지정
                PlaceCard(cards[i], normalizedPosition);
            }
        }

        void DrawArc()
        {
            lineRenderer.positionCount = segments + 1;
            float angleStep = angle / segments;
            float currentAngle = 0f;

            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;
                float y = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;

                lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
                currentAngle += angleStep;
            }
        }

        void PlaceCard(Transform card, float normalizedPosition)
        {
            // 위치를 0~1 값을 이용해 계산
            float cardAngle = normalizedPosition * angle;  // 각도를 호 범위 내에서 설정
            float x = Mathf.Cos(Mathf.Deg2Rad * cardAngle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * cardAngle) * radius;

            card.position = new Vector3(x, y, 0f);  // 카드 위치 지정
        }
    }
}
