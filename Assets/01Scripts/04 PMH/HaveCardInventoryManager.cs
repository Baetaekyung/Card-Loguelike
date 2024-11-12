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

        //public ScrollRect scrollRect;  // ScrollRect 컴포넌트 참조
        private float minScrollY = 0f;       // 스크롤 가능한 최소 Y 위치 (하단)
        private float maxScrollY = 1f;       // 스크롤 가능한 최대 Y 위치 (상단)

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
                //ただ　たくて　泣かないで 프리팹을 조정해서 안됩니다
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
            // 코루틴을 통해 각각의 이미지가 부드럽게 움직이도록 실행
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

            // 이동이 끝나면 최종 위치로 설정

            if(isOpenInventory)
                image.localPosition = startPos;
            else
                image.localPosition = endPos; 
            

            isOpenning = false;
        }
    }
}
