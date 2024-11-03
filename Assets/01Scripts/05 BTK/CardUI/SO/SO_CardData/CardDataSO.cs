using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Card Informations

public enum CardType
{
    /// <enum name="CardType">
    /// 임시 값 추후 기획을 통하여 변경
    /// </enum>
    Passive,
    Skill,
    Buff,
    Debuff
}
public enum CardRarity
{
    Common,
    Rare,
    Epic
}

[Serializable]
public struct CardInfo
{
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    public int cost; //행동력
    public CardRarity cardRarity;
}

#endregion

[CreateAssetMenu(fileName = "CardData_", menuName = "SO/CardDataSO/CardData")]
public class CardDataSO : ScriptableObject
{
    public CardType cardType; //스킬카드냐, 일반 카드냐
    public CardInfo cardInfo;
}
