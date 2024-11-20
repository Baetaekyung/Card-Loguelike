using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class HaveCardInventoryManager : MonoBehaviour
    {
        public static HaveCardInventoryManager Instance;

        public bool isOpenInventory = false;
        private bool isOpenning = false;

        [SerializeField] public Image leftInv;
        [SerializeField] private ScrollRect leftInvScrollRect;

        [SerializeField] private Vector2 beforePos;
        [SerializeField] private Vector2 afterPos;
        [SerializeField] private float duration = 1f;

        private float listYPosModi = 450;

        //public ScrollRect scrollRect;  // ScrollRect ������Ʈ ����
        private float minScrollY = 0f;       // ��ũ�� ������ �ּ� Y ��ġ (�ϴ�)
        private float maxScrollY = 1f;       // ��ũ�� ������ �ִ� Y ��ġ (���)

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

            leftInvScrollRect = leftInv.GetComponent<ScrollRect>();
        }
        void Start()
        {
            ArrangeHaveCards();
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

            RectHahahaha();
        }

        private void RectHahahaha()
        {
            //leftInv.rectTransform.anchoredPosition
        }
        public void ArrangeHaveCards()
        {
            int idx = 1;
            List<CardUI> haveCards = LobbyDeckCardManager.Instance.GetHaveCardList();
            foreach (var item in haveCards)
            {
                //�����������ơ��誫�ʪ��� �������� �����ؼ� �ȵ˴ϴ�
                //Debug.Log(item.name);
                item.transform.SetParent(leftInv.transform, false);
                SetPosition(item, idx);
                idx++;
            }
            SetInventorySize(idx);
        }
        private void SetPosition(CardUI card, int idx)
        {
            card.transform.localPosition = Vector3.zero;
            float modi = leftInv.rectTransform.rect.height;
            modi /= 2;
            card.transform.localPosition += new Vector3(0, (idx * listYPosModi) - modi);
            //
        }
        private void SetInventorySize(int idx)
        {
            float w = leftInv.rectTransform.rect.width;
            leftInv.rectTransform.sizeDelta = new Vector2(w, idx * listYPosModi);
        }

        public void OpenInventory()
        {
            // �ڷ�ƾ�� ���� ������ �̹����� �ε巴�� �����̵��� ����
            isOpenning = true;

            StartCoroutine(MoveToPosition(leftInv.rectTransform, -beforePos, -afterPos, duration));
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
