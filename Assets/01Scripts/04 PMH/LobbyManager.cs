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
                //모든 카드를 가지고 시작하기 때문에 저장할 필요가 없음!
            }
            else
            {
                Debug.Log("나일초과로(Like 나일강 범람) 하여 시작할수없습니다.");
            }
        }
        else
        {
            Debug.Log("아가야 카드 8개 이상 덱에 설정 해야한다고. 8개 이상. 뭔뜻인지 몰라? 설명해줘?");
        }
    }

    public void LoadCardsFromJson()
    {
        //차있으면 안되게
        if (LobbyDeckCardManager.Instance.visualDeckCard.Count > 0) return;
        loadedCard.Clear();

        CardManager.Instance.deckCardList.Clear();

        CardDataManager.Instance.LoadCurrentDeck();

        for(int i = 0; i < CardManager.Instance.deckCardList.Count; i++)
        {
            loadedCard.Add(CardManager.Instance.deckCardList[i]);
        }
        LobbyDeckCardManager.Instance.SetCurrentDeckCardsFromCardSO(loadedCard);
        Debug.Log("로드카드덱하하하하" + loadedCard.Count);
    }
}
