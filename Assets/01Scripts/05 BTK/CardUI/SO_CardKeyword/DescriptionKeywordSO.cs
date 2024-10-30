using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keyword_", menuName = "SO/Keyword/Description_KeywordSO")]
public class DescriptionKeywordSO : ScriptableObject
{
    public string[] keywords;
    public Color    textColor;
    public string   description; //이거 뒤에 /n 붙여서 줄바꿈!
}
