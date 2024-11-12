using CardGame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public void Roses_Favorite_RandomGame_RandomGame_GameStart_Button()
    {
        bool canStart = LobbyDeckCardManager.Instance.IsDeckCardExceed8();

        if (canStart)
        {
            if(LobbyDeckCardManager.Instance.IsArentExceedCost())
            {
                Debug.Log("���� ��ŸƮ : ����Ʈ����Ʈ ����Ʈ����Ʈ ����Ʈ����Ʈ �� �������");
                CardDataManager.Instance.SaveCurrentDeck(LobbyDeckCardManager.Instance.GetCurrentDeckCardsToCardSO());
                CardDataManager.Instance.SaveHavingCard(LobbyDeckCardManager.Instance.GetHavingCardsToCardSO());
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
        List<CardSO> loadedCard = CardDataManager.Instance.LoadCurrentDeck();
        LobbyDeckCardManager.Instance.SetCurrentDeckCardsFromCardSO(loadedCard);
        Debug.Log("�ε�ī�嵦��������" + loadedCard.Count);
    }
}
