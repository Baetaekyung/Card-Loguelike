using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class PushObstacle : MonoBehaviour
    {
        [SerializeField] private float minX = 0, maxX = 17;
        public float duration = 2f; // 이동 및 대기 시간

        private void Start()
        {
            StartCoroutine(MoveCycle());
        }
        private IEnumerator MoveCycle()
        {
            while (true) // 반복 실행
            {
                // 1. maxX로 이동
                yield return MoveToPosition(maxX);
                // 2. 2초 대기
                yield return new WaitForSeconds(duration);
                // 3. minX로 이동
                yield return MoveToPosition(minX);
                // 4. 2초 대기
                yield return new WaitForSeconds(duration);
            }
        }

        private IEnumerator MoveToPosition(float targetX)
        {
            Vector3 startPos = transform.position; // 현재 위치
            Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z); // 목표 위치
            float elapsedTime = 0f;

            // 선형 보간을 이용한 이동
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // 한 프레임 대기
            }

            // 목표 위치에 정확히 도달
            transform.position = targetPos;
        }
    }
}
