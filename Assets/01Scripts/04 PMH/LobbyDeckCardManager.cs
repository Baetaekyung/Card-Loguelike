using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class LobbyDeckCardManager : MonoBehaviour, IPointerDownHandler
    {
        //�ѹ� ���� ���� �ߵ� �ѹ��� �Ƚ����� ������ �ȵ�

        //���� �ƴϰ� ȣ�� ���� �ϴµ� ī�带 ���� �ѱ��� �� ���⼭ �������� �ͼ� �Ҽ��� ����...
        //���־󸮽�Ʈ�� �� ����Ʈ
        //���־� ����Ʈ�� �� �ִ�ġ�� ������ ����.
        //���־� ����Ʈ������ ���� ó�� ������ ��
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
            //��üũ
            //cnt���� ����Ʈ���� �����
            //����� ������� ī�� ����Ʈ�� �ֱ�
            //������� ī�� ����Ʈ���� �Ǿտ��� ���־� ����Ʈ�� �ֱ�
            //��� �������� �ڿ��ž����� ����� ����
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

                // ī�� ���� �� �Ǻ� �������� ��ġ ����
                CardUI card = Instantiate(testAddCard, transform.position, Quaternion.identity);
                card.transform.SetParent(centerTrm, true);
                card.GetRectTransform().anchoredPosition = Vector2.zero; // �Ǻ� ���� ��ġ�� �������� ����
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
                //���� �������� ���������� (�ΰ��� ���� Ʈ��)
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
            // visualDeckCard ����Ʈ ����
            for (int i = 0; i < visualDeckCard.Count - 1; i++)
            {
                if (visualDeckCard[i] == null)
                {
                    // �� �ڸ��� �ļ��� ��ҷ� ä��
                    visualDeckCard[i] = visualDeckCard[i + 1];
                    visualDeckCard[i + 1] = null;
                }
            }

            // ������ ��Ұ� ������� �� �����Ƿ� ����
            if (visualDeckCard[visualDeckCard.Count - 1] == null)
            {
                visualDeckCard.RemoveAt(visualDeckCard.Count - 1);
            }

            // deckCard ����Ʈ ����
            for (int i = 0; i < deckCard.Count - 1; i++)
            {
                if (deckCard[i] == null)
                {
                    // �� �ڸ��� �ļ��� ��ҷ� ä��
                    deckCard[i] = deckCard[i + 1];
                    deckCard[i + 1] = null;
                }
            }

            // ������ ��Ұ� ������� �� �����Ƿ� ����
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
                Vector2 direction = (centerTrm.anchoredPosition).normalized; // �߽��� �ٶ󺸴� ���� ���
                float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���

                // ī���� �ٴڸ��� �߽��� ���ϵ��� ���� (z�� ȸ���� ����)
                visualDeckCard[i].GetRectTransform().eulerAngles = new Vector3(0, 0, angleToCenter + 90); // 180�� �����Ͽ� �ٴڸ��� �߽��� ���ϵ���
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
    }
}
