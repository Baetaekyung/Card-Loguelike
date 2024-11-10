using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class DeckSaveData
{
    public string[] resentDeck;
}

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    public List<CardSO> starterCardPack = new();
    public List<CardSO> starterDeck = new();

    private static DeckSaveData _deckData = new DeckSaveData();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance.gameObject);
    }

    public void LoadHavingCard()
    {
        CardManager.Instance.haveCards = starterCardPack;
    }

    public void SaveCurrentDeck(List<string> choicedDeck)
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        _deckData.resentDeck = choicedDeck.ToArray();
        string json = JsonUtility.ToJson(_deckData, true);
        File.WriteAllText(path, json);
    }

    public void LoadCurrentDeck()
    {
        string path = Path.Combine(Application.persistentDataPath, "deckCards.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            _deckData = JsonUtility.FromJson<DeckSaveData>(jsonData);
            for(int i = 0; i < _deckData.resentDeck.Length; i++)
            {
                CardManager.Instance.AddToDeckCard(
                    CardManager.Instance.nameByDictionary[_deckData.resentDeck[i]]);
            }
        }
        else
        {
            List<string> tempList = new List<string>();
            for(int i = 0; i < starterDeck.Count; i++)
            {
                tempList.Add(starterDeck[i].cardObject.CardInfo.cardName);
            }
            SaveCurrentDeck(tempList);

            for (int i = 0; i < _deckData.resentDeck.Length; i++)
            {
                CardManager.Instance.AddToDeckCard(
                    CardManager.Instance.nameByDictionary[_deckData.resentDeck[i]]);
            }
        }
    }
}
