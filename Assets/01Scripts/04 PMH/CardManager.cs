using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Selection, Equip
}
public class Card : MonoBehaviour { }

public class CardManager : MonoBehaviour
{
    private List<Card> cardLists;
    private Dictionary<CardType, Card> _card;

    private void Awake()
    {
        
    }

    public void SortingEquipCard()
    {
        foreach(var item in cardLists)
        {
            
        }
    }
    public void SortingSelectionCard()
    {
        foreach (var item in cardLists)
        {

        }
    }
}
