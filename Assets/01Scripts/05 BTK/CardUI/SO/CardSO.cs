using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_", menuName ="SO/CardDataSO/Card")]
public class CardSO : ScriptableObject
{
    public CardUI cardUI;
    public CardObject cardObject;
}
