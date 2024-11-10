using CardGame;
using UnityEngine;

[CreateAssetMenu(fileName = "Keyword_", menuName = "SO/Keyword/Description_KeywordSO")]
public class DescriptionKeywordSO : ScriptableObject
{
    public KeywordEnum keyword;
    public Color textColor;
    public string description;
}
