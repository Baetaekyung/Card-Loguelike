using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordList", menuName = "SO/Keyword/KeywordList")]
public class KeywordListSO : ScriptableObject
{
    //이거 계속 돌면서 단어 있는지 찾기
    public List<DescriptionKeywordSO> keywordList;
    private Dictionary<string, Color> _colorDictionary;
    private Dictionary<string, string> _descriptionDictionary;

    private bool _isInited = false;

    public void InitDictionary()
    {
        if (_isInited) return;

        foreach (var keywordSO in keywordList)
        {
            foreach (var keyword in keywordSO.keywords)
            {
                _colorDictionary.Add(keyword, keywordSO.textColor);
                _descriptionDictionary.Add(keyword, keywordSO.description);
            }
        }
    }

    public string GetKeywordDescription(string keyword)
    {
        return _descriptionDictionary[keyword];
    }

    public string CombineDescription(string origin, string newText)
    {
        StringBuilder sb = new StringBuilder();
        origin = origin.Replace("\\n", "\n");
        newText = newText.Replace("\\n", "\n");
        sb.Append(origin).Append(newText);

        return sb.ToString();
    }

    ///<summary>
    ///설명을 넣으면 키워드가 존재하는지 알려주고, 존재하는 키워드들을 리스트로 반환해줌
    ///</summary>
    public (bool exist, List<string> keywords) ExistKeywords(string description) // 튜플 반환
    {
        List<string> keywords = new List<string>();

        foreach (var keywordSO in keywordList)
        {
            foreach (var keyword in keywordSO.keywords)
            {
                if (description.Contains(keyword))
                    keywords.Add(keyword);
            }
        }

        if (keywords.Count > 0)
            return (true, keywords);
        else
            return (false, null); //설명에 포함 중인 키워드들 반환
    }

    //결합된 문자열을 넣어야해요
    public string AddKeywordAccentIfExist(string combinedDescription)
    {
        if (ExistKeywords(combinedDescription).Item1)
        {
            string replacedDescription = "";
            for (int i = 0; i < ExistKeywords(combinedDescription).keywords.Count; i++)
            {
                string keyword = ExistKeywords(combinedDescription).keywords[i]; //키워드 받아오기
                string colorCode = ColorUtility.ToHtmlStringRGB(_colorDictionary[keyword]);
                #region colorCode Description
                ///<param name="colorCode">
                ///if color == Color.Red
                ///return colorCode = "FF0000"
                ///</param> 
                #endregion

                replacedDescription = combinedDescription.Replace(keyword,
                    $"<color=#{colorCode}>{keyword}</color>");
            }
            //스트링 문자열에 원래 키워드를 마크다운 문법을 통하여 색깔을 넣어준다.
            return replacedDescription;
        }

        //키워드가 포함된 문자열이 아니라면 기존 문자열 반환
        return combinedDescription;
    }
}
