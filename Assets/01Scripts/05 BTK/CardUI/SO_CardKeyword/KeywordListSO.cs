using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordList", menuName = "SO/Keyword/KeywordList")]
public class KeywordListSO : ScriptableObject
{
    //�̰� ��� ���鼭 �ܾ� �ִ��� ã��
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
    ///������ ������ Ű���尡 �����ϴ��� �˷��ְ�, �����ϴ� Ű������� ����Ʈ�� ��ȯ����
    ///</summary>
    public (bool exist, List<string> keywords) ExistKeywords(string description) // Ʃ�� ��ȯ
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
            return (false, null); //���� ���� ���� Ű����� ��ȯ
    }

    //���յ� ���ڿ��� �־���ؿ�
    public string AddKeywordAccentIfExist(string combinedDescription)
    {
        if (ExistKeywords(combinedDescription).Item1)
        {
            string replacedDescription = "";
            for (int i = 0; i < ExistKeywords(combinedDescription).keywords.Count; i++)
            {
                string keyword = ExistKeywords(combinedDescription).keywords[i]; //Ű���� �޾ƿ���
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
            //��Ʈ�� ���ڿ��� ���� Ű���带 ��ũ�ٿ� ������ ���Ͽ� ������ �־��ش�.
            return replacedDescription;
        }

        //Ű���尡 ���Ե� ���ڿ��� �ƴ϶�� ���� ���ڿ� ��ȯ
        return combinedDescription;
    }
}
