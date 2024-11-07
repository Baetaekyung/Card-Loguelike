using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class LobbyDeckCardManager : MonoBehaviour, IPointerDownHandler
    {
        //한번 쓰면 정렬 잘됨 한번도 안썼을시 정렬이 안됨

        //원이 아니고 호를 만들어서 하는데 카드를 어케 넘길지 걍 여기서 뇌정지가 와서 할수가 없음...
        //비주얼리스트와 덱 리스트
        //비주얼 리스트에 는 최대치가 정해져 있음.
        //비주얼 리스트에서는 지금 처럼 원으로 돎
        [SerializeField] private int deckCardMaxCost;
        [SerializeField] private CardUI testAddCard;

        public List<CardUI> visualDeckCard;
        public List<CardUI> haveCard;
        public List<CardUI> deckCard;

        public Vector2 centerPos;
        public RectTransform centerTrm;
        public int visualDeckCardsAmount = 8;

        public float radius = 5f;

        public TMP_Text deckCardCostText;
        void Start()
        {
            //ming.Remove(ming[1]);
            //널체크
            //cnt값인 리스트원소 지우기
            //지운거 대기중인 카드 리스트에 넣기
            //대기중인 카드 리스트에서 맨앞에거 비주얼 리스트에 넣기
            //빈곳 생겼으니 뒤에거앞으로 떙기는 정렬
            //
            //haveCard = CardDataManager.Instance.LoadHavingCard();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                centerPos = centerTrm.anchoredPosition;
                foreach(var item in visualDeckCard)
                {
                    item.transform.SetParent(centerTrm, true);
                }
                ArrangeCards();
                AddCardAndRemoveCard();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                centerPos = centerTrm.anchoredPosition;

                // 카드 생성 후 피봇 기준으로 위치 설정
                CardUI card = Instantiate(testAddCard, transform.position, Quaternion.identity);
                card.transform.SetParent(centerTrm, true);
                card.GetRectTransform().anchoredPosition = Vector2.zero; // 피봇 기준 위치를 원점으로 설정
                visualDeckCard.Add(card);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
            SetCardsAngle();
        }

        private void AddCardAndRemoveCard()
        {
            if (visualDeckCard == null || visualDeckCard.Count == 0) return;

            int cc = visualDeckCard.Count;
            if(cc < visualDeckCardsAmount)
            {
                //만약 보여지고 내려갔으면 (두개의 값이 트루)
                if(visualDeckCard[0].throwGround && visualDeckCard[0].throwUnder)
                {
                    visualDeckCard[0].transform.position = centerTrm.position;

                    visualDeckCard[0].throwGround = false;
                    visualDeckCard[0].throwUnder = false;

                    deckCard.Add(visualDeckCard[0]);
                    visualDeckCard.Add(deckCard[0]);

                    visualDeckCard[0].gameObject.SetActive(true);

                    deckCard.Remove(deckCard[0]);
                    visualDeckCard.Remove(visualDeckCard[0]);

                    SortingCardList();
                }
            }
        }

        private void SortingCardList()
        {
            // visualDeckCard 리스트 정렬
            for (int i = 0; i < visualDeckCard.Count - 1; i++)
            {
                if (visualDeckCard[i] == null)
                {
                    // 빈 자리를 후순위 요소로 채움
                    visualDeckCard[i] = visualDeckCard[i + 1];
                    visualDeckCard[i + 1] = null;
                }
            }

            // 마지막 요소가 비어있을 수 있으므로 제거
            if (visualDeckCard[visualDeckCard.Count - 1] == null)
            {
                visualDeckCard.RemoveAt(visualDeckCard.Count - 1);
            }

            // deckCard 리스트 정렬
            for (int i = 0; i < deckCard.Count - 1; i++)
            {
                if (deckCard[i] == null)
                {
                    // 빈 자리를 후순위 요소로 채움
                    deckCard[i] = deckCard[i + 1];
                    deckCard[i + 1] = null;
                }
            }

            // 마지막 요소가 비어있을 수 있으므로 제거
            if (deckCard[deckCard.Count - 1] == null)
            {
                deckCard.RemoveAt(deckCard.Count - 1);
            }
        }

        private void SetCardsAngle()
        {
            int cardCount = visualDeckCard.Count;

            if (cardCount == 0) return;

            for (int i = 0; i < cardCount; i++)
            {
                Vector2 direction = (centerTrm.anchoredPosition).normalized; // 중심을 바라보는 방향 계산
                float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산

                // 카드의 바닥면이 중심을 향하도록 설정 (z축 회전만 적용)
                visualDeckCard[i].GetRectTransform().eulerAngles = new Vector3(0, 0, angleToCenter + 90); // 180도 보정하여 바닥면이 중심을 향하도록
            }
        }
        public bool IsExceedMaxCost(CardSO newCard)
        {
            int nowCost = 0;
            int newCostModifire = newCard.cardObject.GetCardData().cardInfo.cost;
            foreach(var item in visualDeckCard)
            {
                //nowCost += item.cardObject.GetCardData().cardInfo.cost;
            }

            return deckCardMaxCost < nowCost + newCostModifire;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HaveCardInventoryManager.Instance.isOpenInventory = !HaveCardInventoryManager.Instance.isOpenInventory;
        }

        private void ArrangeCards()
        {
            int cardCount = visualDeckCard.Count;

            if (cardCount == 0) return;

            //float angModi = 1;

            //if (cardCount % 4 == 0 || cardCount > 3)
            //{
            //    angModi = cardCount / 4;
            //    if (angModi == 0) angModi = 1;
            //    //SetCenterTrmPosition(-(angModi * 100));
            //}

            // 각 카드 간의 각도 간격을 카드 개수에 따라 동적으로 계산
            float angleIncrement = 360f / cardCount;

            for (int i = 0; i < cardCount; i++)
            {
                // 각 카드의 위치에 따라 각도 계산
                float angle = angleIncrement * i;
                float radian = angle * Mathf.Deg2Rad;

                // 카드의 위치 계산 (피봇 기준 배치)
                Vector2 cardPos = new Vector2(
                    radius * Mathf.Cos(radian),
                    radius * Mathf.Sin(radian)
                );

                // 피봇을 기준으로 카드 배치
                visualDeckCard[i].GetRectTransform().anchoredPosition = cardPos; //cardPos * (angModi * 0.7f)

                // 각도를 2D 공간에서 중심을 바라보도록 조정
                Vector2 direction = (centerTrm.anchoredPosition).normalized; // 중심을 바라보는 방향 계산
                float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산

                // 카드의 바닥면이 중심을 향하도록 설정 (z축 회전만 적용)
                visualDeckCard[i].GetRectTransform().eulerAngles = new Vector3(0, 0, angleToCenter + 90); // 180도 보정하여 바닥면이 중심을 향하도록
            }
        }
    }
}
