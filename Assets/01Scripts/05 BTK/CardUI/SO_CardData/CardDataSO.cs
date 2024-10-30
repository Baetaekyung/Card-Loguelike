using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Card Informations

public enum CardType
{
    /// <enum name="CardType">
    /// �ӽ� �� ���� ��ȹ�� ���Ͽ� ����
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
    public string cardEffectDescription;
    public Sprite cardSprite;
    public int cost; //�ൿ��
    public CardRarity cardRarity;
}

#endregion

[CreateAssetMenu(fileName = "CardData_", menuName = "SO/CardDataSO/CardData")]
public class CardDataSO : ScriptableObject
{
    public CardType cardType; //��ųī���, �Ϲ� ī���
    public CardInfo cardInfo;
    public KeywordListSO keywordList;
}
