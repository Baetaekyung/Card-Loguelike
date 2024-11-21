using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class PushObstacle : MonoBehaviour
    {
        [SerializeField] private float minX = 0, maxX = 17;
        public float duration = 2f; // �̵� �� ��� �ð�

        private void Start()
        {
            StartCoroutine(MoveCycle());
        }
        private IEnumerator MoveCycle()
        {
            while (true) // �ݺ� ����
            {
                // 1. maxX�� �̵�
                yield return MoveToPosition(maxX);
                // 2. 2�� ���
                yield return new WaitForSeconds(duration);
                // 3. minX�� �̵�
                yield return MoveToPosition(minX);
                // 4. 2�� ���
                yield return new WaitForSeconds(duration);
            }
        }

        private IEnumerator MoveToPosition(float targetX)
        {
            Vector3 startPos = transform.position; // ���� ��ġ
            Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z); // ��ǥ ��ġ
            float elapsedTime = 0f;

            // ���� ������ �̿��� �̵�
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // �� ������ ���
            }

            // ��ǥ ��ġ�� ��Ȯ�� ����
            transform.position = targetPos;
        }
    }
}
