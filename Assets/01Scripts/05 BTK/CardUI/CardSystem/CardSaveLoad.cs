using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct CardSaveData
{
    public List<CardSO> cardSODatas;
    public List<BaseCard> resentDeck;
}

public class CardSaveLoad : MonoBehaviour
{
    public static CardSaveLoad Instance;

    public List<CardSO> starterCardPack = new();
    public List<BaseCard> starterDeck = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);
    }

    public void SaveHavingCard(List<CardSO> haveCards)
    {
        string path = Path.Combine(Application.persistentDataPath, "haveCards.json");
        CardSaveData data = new CardSaveData();

        data.cardSODatas = haveCards;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
    
    public List<CardSO> LoadHavingCard()
    {
        string path = Path.Combine(Application.persistentDataPath, "haveCards.json");

        CardSaveData data = new CardSaveData();
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<CardSaveData>(jsonData);
        }
        else
        {
            data.cardSODatas = starterCardPack;
            SaveHavingCard(starterCardPack);
        }

        return data.cardSODatas;
    }

    public void SaveCurrentDeck(List<BaseCard> choicedDeck)
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        CardSaveData data = new CardSaveData();
        data.resentDeck = choicedDeck;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }

    public List<BaseCard> LoadCurrentDeck()
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        CardSaveData data = new CardSaveData();
        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<CardSaveData>(jsonData);
        }
        else
        {
            data.resentDeck = starterDeck;
            SaveCurrentDeck(starterDeck);
        }

        return data.resentDeck;
    }
}
