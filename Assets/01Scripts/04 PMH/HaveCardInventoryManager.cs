using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class HaveCardInventoryManager : MonoBehaviour
    {
        public static HaveCardInventoryManager Instance;

        public bool isOpenInventory = false;
        private bool isOpenning = false;

        [SerializeField] private Image leftInv, rightInv;
        [SerializeField] private Vector2 beforePos;
        [SerializeField] private Vector2 afterPos;
        [SerializeField] private float duration = 1f;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && isOpenning == false)
            {
                //isOpenInventory
                isOpenInventory = !isOpenInventory;
                OpenInventory();
            }
        }

        public void OpenInventory()
        {
            // �ڷ�ƾ�� ���� ������ �̹����� �ε巴�� �����̵��� ����
            isOpenning = true;

            StartCoroutine(MoveToPosition(leftInv.rectTransform, -beforePos, -afterPos, duration));
            StartCoroutine(MoveToPosition(rightInv.rectTransform, beforePos, afterPos, duration));
        }

        private IEnumerator MoveToPosition(RectTransform image, Vector3 startPos, Vector3 endPos, float time)
        {
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                if(isOpenInventory)
                    image.localPosition = Vector2.Lerp(endPos, startPos, elapsedTime / time);
                else
                    image.localPosition = Vector2.Lerp(startPos, endPos, elapsedTime / time);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �̵��� ������ ���� ��ġ�� ����

            if(isOpenInventory)
                image.localPosition = startPos;
            else
                image.localPosition = endPos; 
            

            isOpenning = false;
        }
    }
}
