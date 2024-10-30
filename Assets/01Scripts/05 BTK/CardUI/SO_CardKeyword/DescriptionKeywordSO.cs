using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keyword_", menuName = "SO/Keyword/Description_KeywordSO")]
public class DescriptionKeywordSO : ScriptableObject
{
    public string[] keywords;
    public Color    textColor;
    public string   description; //�̰� �ڿ� /n �ٿ��� �ٹٲ�!
}
