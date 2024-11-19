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
        public static LobbyDeckCardManager Instance; //�̾��ϴ�...

        //�ѹ� ���� ���� �ߵ� �ѹ��� �Ƚ����� ������ �ȵ�

        //���� �ƴϰ� ȣ�� ���� �ϴµ� ī�带 ���� �ѱ��� �� ���⼭ �������� �ͼ� �Ҽ��� ����...
        //���־󸮽�Ʈ�� �� ����Ʈ
        //���־� ����Ʈ�� �� �ִ�ġ�� ������ ����.
        //���־� ����Ʈ������ ���� ó�� ������ ��
        [SerializeField] private int deckCardMaxCost;
        [SerializeField] private CardUI testAddCard;

        [SerializeField] private TMP_Text costAmountText;

        public List<CardUI> visualDeckCard;

        public List<string> visualDeckCard_CardSO;

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

        private Color originalColor;      // ���� ���� ����
        private Vector3 originalScale;    // ���� ũ�� ����

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(Instance);

            originalColor = costAmountText.color; // ���� ���� �ʱ�ȭ
            originalScale = costAmountText.transform.localScale; // ���� ũ�� �ʱ�ȭ
            //ming.Remove(ming[1]);
            //��üũ
            //cnt���� ����Ʈ���� �����
            //����� ������� ī�� ����Ʈ�� �ֱ�
            //������� ī�� ����Ʈ���� �Ǿտ��� ���־� ����Ʈ�� �ֱ�
            //��� �������� �ڿ��ž����� ����� ����
            //
            //haveCard = CardDataManager.Instance.LoadHavingCard();

            //prefabHaveCards_CardSO = CardDataManager.Instance.LoadHavingCard(); //���� �̰� �ؾߵǴµ� ���׳� ���̽����� �ȳ��»���� ���ڵ��� �ּ��� Ǫ�ʽÿ�

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
            //    // ī�� ���� �� �Ǻ� �������� ��ġ ����
            //    CardUI card = Instantiate(testAddCard, transform.position, Quaternion.identity);
            //    card.transform.SetParent(centerTrm, true);
            //    card.GetRectTransform().anchoredPosition = Vector2.zero; // �Ǻ� ���� ��ġ�� �������� ����
            //    visualDeckCard.Add(card);
            //}
            SetCardsAngle();
        }
        //��ī�帮��Ʈ�� �ִ°Ŵ�� SO����Ʈ �����ϰ� ������ŭ ��������
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

        public List<string> GetCurrentDeckCardsToCardSO()
        {
            visualDeckCard_CardSO.Clear();

            foreach (var item in visualDeckCard)
            {
                visualDeckCard_CardSO.Add(item.CardInfo.cardName);
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
        public void SetCurrentDeckCardsFromCardSO(List<CardSO> cards) //ī�� Json���� �ҷ����°��� �ҷ��������� �� ����� ������� ����
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
                        j--; // ����Ʈ ũ�� ��ȭ�� ���� �ε��� ����
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
            // ��ȿ�� �˻�: ī�尡 null�� ��� �Լ� ����
            if (card == null) return;

            // ī�� �߰� �� ����Ʈ ������Ʈ
            AddCardToDeck(card);
            HaveCardInventoryManager.Instance.ArrangeHaveCards(); // �κ��丮 ����
            SettingCleanningArrangeCard(); // ��Ÿ �߰� �۾�
        }
        public bool IsArentExceed(CardUI card)
        {
            // ��� ���� �˻�
            int currentCost = GetCostAmount();
            if (currentCost + card.CardInfo.cost > MAX_COST)
            {
                AttentionText(); // ��� �ʰ� �� �ð��� ȿ�� ǥ��
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
            // ī�� ���� �� �θ� ����
            card.transform.SetParent(centerTrm, true);

            // ī�� ����Ʈ ������Ʈ
            haveCard.Remove(card);
            visualDeckCard.Add(card);

            Debug.Log("ī�尡 �߰��Ǿ����ϴ�.");
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
            // �ؽ�Ʈ�� ���������� ����
            costAmountText.color = Color.red;

            // ũ�⸦ 1.5��� Ȯ��
            costAmountText.transform.localScale = originalScale * 1.5f;

            // 1�� ���� ����
            yield return new WaitForSeconds(1f);

            // ���� ����� ũ��� ����
            costAmountText.color = originalColor;
            costAmountText.transform.localScale = originalScale;
        }

        private void SetCardsAngle()
        {
            int cardCount = visualDeckCard.Count;

            if (cardCount == 0) return;

            for (int i = 0; i < cardCount; i++)
            {
                Vector2 direction = (centerTrm.anchoredPosition).normalized; // �߽��� �ٶ󺸴� ���� ���
                float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���

                // ī���� �ٴڸ��� �߽��� ���ϵ��� ���� (z�� ȸ���� ����)
                visualDeckCard[i].GetRectTransform().eulerAngles = new Vector3(0, 0, angleToCenter + 90); // 180�� �����Ͽ� �ٴڸ��� �߽��� ���ϵ���
            }
        }
        public bool IsExceedMaxCost(CardSO newCard)
        {
            int nowCost = 0;
//i changed Getter (function) to getter property. blame viet tae gyong
            int newCostModifire = newCard.cardObject.CardData.cardInfo.cost;
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

            // �� ī�� ���� ���� ������ ī�� ������ ���� �������� ���
            float angleIncrement = 360f / cardCount;

            for (int i = 0; i < cardCount; i++)
            {
                // �� ī���� ��ġ�� ���� ���� ���
                float angle = angleIncrement * i;
                float radian = angle * Mathf.Deg2Rad;

                // ī���� ��ġ ��� (�Ǻ� ���� ��ġ)
                Vector2 cardPos = new Vector2(
                    radius * Mathf.Cos(radian),
                    radius * Mathf.Sin(radian)
                );

                // �Ǻ��� �������� ī�� ��ġ
                visualDeckCard[i].GetRectTransform().anchoredPosition = cardPos; //cardPos * (angModi * 0.7f)

                // ������ 2D �������� �߽��� �ٶ󺸵��� ����
                Vector2 direction = (centerTrm.anchoredPosition).normalized; // �߽��� �ٶ󺸴� ���� ���
                float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���

                // ī���� �ٴڸ��� �߽��� ���ϵ��� ���� (z�� ȸ���� ����)
                visualDeckCard[i].GetRectTransform().eulerAngles = new Vector3(0, 0, angleToCenter + 90); // 180�� �����Ͽ� �ٴڸ��� �߽��� ���ϵ���
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
