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
                Debug.Log("게임 스타트 : 아파트아파트 아파트아파트 아파트아파트 어 어허어허");
                CardDataManager.Instance.SaveCurrentDeck(LobbyDeckCardManager.Instance.GetCurrentDeckCardsToCardSO());
                CardDataManager.Instance.SaveHavingCard(LobbyDeckCardManager.Instance.GetHavingCardsToCardSO());
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
        List<CardSO> loadedCard = CardDataManager.Instance.LoadCurrentDeck();
        LobbyDeckCardManager.Instance.SetCurrentDeckCardsFromCardSO(loadedCard);
        Debug.Log("로드카드덱하하하하" + loadedCard.Count);
    }
}
