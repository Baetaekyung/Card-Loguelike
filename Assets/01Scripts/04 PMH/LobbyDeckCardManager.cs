using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class LobbyDeckCardManager : MonoBehaviour, IPointerDownHandler
    {
        public static LobbyDeckCardManager Instance; //미안하다...

        //한번 쓰면 정렬 잘됨 한번도 안썼을시 정렬이 안됨

        //원이 아니고 호를 만들어서 하는데 카드를 어케 넘길지 걍 여기서 뇌정지가 와서 할수가 없음...
        //비주얼리스트와 덱 리스트
        //비주얼 리스트에 는 최대치가 정해져 있음.
        //비주얼 리스트에서는 지금 처럼 원으로 돎
        [SerializeField] private int deckCardMaxCost;
        [SerializeField] private CardUI testAddCard;

        [SerializeField] private TMP_Text costAmountText;

        public List<CardUI> visualDeckCard;

        public List<CardSO> visualDeckCard_CardSO;

        public List<CardUI> haveCard;

        public List<CardSO> haveCard_CardSO;

        //public List<CardUI> deckCard;

        public List<CardUI> prefabHaveCards_CardUI;
        public List<CardSO> prefabHaveCards_CardSO;

        public Vector2 centerPos;
        public RectTransform centerTrm;
        public int visualDeckCardsAmount = 8;

        public float radius = 5f;
        private const int MAX_COST = 20;
        private const int MIN_CARD = 8;

        private Color originalColor;      // 원래 색상 저장
        private Vector3 originalScale;    // 원래 크기 저장

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(Instance);

            originalColor = costAmountText.color; // 원래 색상 초기화
            originalScale = costAmountText.transform.localScale; // 원래 크기 초기화
            //ming.Remove(ming[1]);
            //널체크
            //cnt값인 리스트원소 지우기
            //지운거 대기중인 카드 리스트에 넣기
            //대기중인 카드 리스트에서 맨앞에거 비주얼 리스트에 넣기
            //빈곳 생겼으니 뒤에거앞으로 떙기는 정렬
            //
            //haveCard = CardDataManager.Instance.LoadHavingCard();

            //prefabHaveCards_CardSO = CardDataManager.Instance.LoadHavingCard(); //원래 이거 해야되는데 버그남 제이슨버그 안나는사람은 이코드의 주석을 푸십시오

            prefabHaveCards_CardUI.Clear();

            foreach(CardSO item in  prefabHaveCards_CardSO)
            {
                CardUI insCard = item.cardUI;
                prefabHaveCards_CardUI.Add(insCard);
            }

            IntantiateHaveCards();
        }

        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    centerPos = centerTrm.anchoredPosition;
            //
            //    // 카드 생성 후 피봇 기준으로 위치 설정
            //    CardUI card = Instantiate(testAddCard, transform.position, Quaternion.identity);
            //    card.transform.SetParent(centerTrm, true);
            //    card.GetRectTransform().anchoredPosition = Vector2.zero; // 피봇 기준 위치를 원점으로 설정
            //    visualDeckCard.Add(card);
            //}
            SetCardsAngle();
        }
        //덱카드리스트에 있는거대로 SO리스트 정렬하고 갯수만큼 가져가기
        private void IntantiateHaveCards()
        {
            int n = 0;
            foreach(var card in prefabHaveCards_CardUI)
            {
                CardUI added = Instantiate(card, centerTrm.position, Quaternion.identity);
                added.cardCnt = n++;
                haveCard.Add(added);
            }
        }

        public List<CardSO> GetCurrentDeckCardsToCardSO()
        {
            visualDeckCard_CardSO.Clear();

            foreach (var item in visualDeckCard)
            {
                visualDeckCard_CardSO.Add(prefabHaveCards_CardSO[item.cardCnt]);
            }

            return visualDeckCard_CardSO;
        }
        public List<CardSO> GetHavingCardsToCardSO()
        {
            haveCard_CardSO.Clear();

            foreach(var item in haveCard)
            {
                haveCard_CardSO.Add(prefabHaveCards_CardSO[item.cardCnt]);
            }
            return haveCard_CardSO;
        }
        public void SetCurrentDeckCardsFromCardSO(List<CardSO> cards) //카드 Json에서 불러오는거임 불러오기전에 함 비워줌 장비우듯이 ㅇㅇ
        {
            /*for (int i = 0; i < cards.Count; i++)
            {
                string setCard = haveCard[i].name;
                string getCard = cards[i].cardUI.name;

                string[] setCardArr = setCard.Split('(');
                setCard = setCardArr[0];

                string[] getCardArr = getCard.Split('(');
                getCard = getCardArr[0];

                if(setCard == getCard)
                {
                    foreach(var item in haveCard)
                    {
                        if(item.name == setCard)
                        {
                            RemoveCardOfDeckCardList(item);
                        }
                    }
                }
            }*/

            if (visualDeckCard.Count > 0) return;

            visualDeckCard.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                CardUI cardUI = Instantiate(cards[i].cardUI, transform.position, transform.rotation);

                AddCardToDeckCardList(cardUI);
                cardUI.IsInDeckCards = true;
            }

            for (int i = 0; i < visualDeckCard.Count; i++)
            {
                for (int j = 0; j < haveCard.Count; j++)
                {
                    if (visualDeckCard[i].ToString() == haveCard[j].ToString())
                    {
                        haveCard.RemoveAt(j);
                        j--; // 리스트 크기 변화에 따른 인덱스 조정
                    }
                }
            }

        }
        public List<CardUI> GetHaveCardList()
        {
            return haveCard;
        }
        private void SettingCleanningArrangeCard()
        {
            centerPos = centerTrm.anchoredPosition;
            foreach (var item in visualDeckCard)
            {
                item.transform.SetParent(centerTrm, true);
            }
            ArrangeCards();
            //AddCardAndRemoveCard();
            SetCostAmountText();
        }
        public void AddCardToDeckCardList(CardUI card)
        {
            // 유효성 검사: 카드가 null인 경우 함수 종료
            if (card == null) return;

            // 카드 추가 및 리스트 업데이트
            AddCardToDeck(card);
            HaveCardInventoryManager.Instance.ArrangeHaveCards(); // 인벤토리 정렬
            SettingCleanningArrangeCard(); // 기타 추가 작업
        }
        public bool IsArentExceed(CardUI card)
        {
            // 비용 제한 검사
            int currentCost = GetCostAmount();
            if (currentCost + card.CardInfo.cost > MAX_COST)
            {
                AttentionText(); // 비용 초과 시 시각적 효과 표시
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsDeckCardExceed8()
        {
            if(visualDeckCard.Count >= 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsArentExceedCost()
        {
            bool ming = GetCostAmount() <= MAX_COST;

            return ming;
        }
        private void AddCardToDeck(CardUI card)
        {
            // 카드 복제 및 부모 설정
            card.transform.SetParent(centerTrm, true);

            // 카드 리스트 업데이트
            haveCard.Remove(card);
            visualDeckCard.Add(card);

            Debug.Log("카드가 추가되었습니다.");
        }

        public void RemoveCardOfDeckCardList(CardUI card)
        {
            card.transform.SetParent(HaveCardInventoryManager.Instance.leftInv.transform, true);
            visualDeckCard.Remove(card);

            haveCard.Add(card);

            HaveCardInventoryManager.Instance.ArrangeHaveCards();
            SettingCleanningArrangeCard();
        }

        private void AttentionText()
        {
            StartCoroutine(FlashAndEnlarge());
        }
        private IEnumerator FlashAndEnlarge()
        {
            // 텍스트를 빨간색으로 변경
            costAmountText.color = Color.red;

            // 크기를 1.5배로 확대
            costAmountText.transform.localScale = originalScale * 1.5f;

            // 1초 동안 유지
            yield return new WaitForSeconds(1f);

            // 원래 색상과 크기로 복원
            costAmountText.color = originalColor;
            costAmountText.transform.localScale = originalScale;
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

        private void SetCostAmountText()
        {
            int cost = GetCostAmount();
            costAmountText.text = $"{cost} / {MAX_COST}";
        }

        private int GetCostAmount()
        {
            int idx = 0;

            foreach (var item in visualDeckCard)
            {
                idx += item.CardInfo.cost;
            }

            return idx;
        }
    }
}
