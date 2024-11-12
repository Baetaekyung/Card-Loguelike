using UnityEngine;

namespace CardGame
{
    public class TestCardInArc : MonoBehaviour
    {
        public float radius = 5f;               // ȣ�� ������
        public float angle = 90f;               // ��ü ȣ�� ���� (0~360)
        public int segments = 50;               // ȣ�� ������ ���� ����
        public Transform[] cards;               // ��ġ�� ī����� Transform �迭

        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            DrawArc();

            // �� ī�带 ȣ�� ��ġ
            for (int i = 0; i < cards.Length; i++)
            {
                float normalizedPosition = (float)i / (cards.Length - 1);  // 0~1 ������ ������ ī�� ��ġ ����
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
            // ��ġ�� 0~1 ���� �̿��� ���
            float cardAngle = normalizedPosition * angle;  // ������ ȣ ���� ������ ����
            float x = Mathf.Cos(Mathf.Deg2Rad * cardAngle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * cardAngle) * radius;

            card.position = new Vector3(x, y, 0f);  // ī�� ��ġ ����
        }
    }
}
