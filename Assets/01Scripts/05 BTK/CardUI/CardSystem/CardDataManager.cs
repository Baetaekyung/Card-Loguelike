using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class CardSaveData
{
    public CardSO[] cardSODatas;
    public CardSO[] resentDeck;
}

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    public List<CardSO> starterCardPack = new();
    public List<CardSO> starterDeck = new();

    private CardSaveData _data = new CardSaveData();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);
    }

    public void SaveHavingCard(List<CardSO> haveCards)
    {
        string path = Path.Combine(Application.persistentDataPath, "haveCards.json");

        _data.cardSODatas = haveCards.ToArray();

        string json = JsonUtility.ToJson(_data, true);

        File.WriteAllText(path, json);
    }
    
    public List<CardSO> LoadHavingCard()
    {
        string path = Path.Combine(Application.persistentDataPath, "haveCards.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            _data = JsonUtility.FromJson<CardSaveData>(jsonData);

            foreach(var data in _data.cardSODatas)
            {
                
            }
        }
        else
        {
            Debug.Log("Generated new haveCard data");
            _data.cardSODatas = starterCardPack.ToArray();
            SaveHavingCard(starterCardPack);
        }

        return _data.cardSODatas.ToList();
    }

    public void SaveCurrentDeck(List<CardSO> choicedDeck)
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        _data.resentDeck = choicedDeck.ToArray();

        string json = JsonUtility.ToJson(_data, true);

        Debug.Log($"Save current Deck at {path}");
        File.WriteAllText(path, json);
    }

    public List<CardSO> LoadCurrentDeck()
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            _data = JsonUtility.FromJson<CardSaveData>(jsonData);
        }
        else
        {
            _data.resentDeck = starterDeck.ToArray();
            SaveCurrentDeck(starterDeck);
        }

        return _data.resentDeck.ToList();
    }
}
