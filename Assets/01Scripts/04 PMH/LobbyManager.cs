using CardGame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private List<CardSO> loadedCard = new();

    public void Roses_Favorite_RandomGame_RandomGame_GameStart_Button()
    {
        bool canStart = LobbyDeckCardManager.Instance.IsDeckCardExceed8();

        if (canStart)
        {
            if(LobbyDeckCardManager.Instance.IsArentExceedCost())
            {
                CardDataManager.Instance.SaveCurrentDeck(LobbyDeckCardManager.Instance.GetCurrentDeckCardsToCardSO());
                //CardDataManager.Instance.SaveHavingCard(LobbyDeckCardManager.Instance.GetHavingCardsToCardSO());
                //��� ī�带 ������ �����ϱ� ������ ������ �ʿ䰡 ����!
            }
            else
            {
                Debug.Log("�����ʰ���(Like ���ϰ� ����) �Ͽ� �����Ҽ������ϴ�.");
            }
        }
        else
        {
            Debug.Log("�ư��� ī�� 8�� �̻� ���� ���� �ؾ��Ѵٰ�. 8�� �̻�. �������� ����? ��������?");
        }
    }

    public void LoadCardsFromJson()
    {
        //�������� �ȵǰ�
        if (LobbyDeckCardManager.Instance.visualDeckCard.Count > 0) return;
        loadedCard.Clear();

        CardManager.Instance.deckCardList.Clear();

        CardDataManager.Instance.LoadCurrentDeck();

        for(int i = 0; i < CardManager.Instance.deckCardList.Count; i++)
        {
            loadedCard.Add(CardManager.Instance.deckCardList[i]);
        }
        LobbyDeckCardManager.Instance.SetCurrentDeckCardsFromCardSO(loadedCard);
        Debug.Log("�ε�ī�嵦��������" + loadedCard.Count);
    }
}
