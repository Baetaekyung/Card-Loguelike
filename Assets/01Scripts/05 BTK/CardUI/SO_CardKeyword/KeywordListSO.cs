using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordList", menuName = "SO/Keyword/KeywordList")]
public class KeywordListSO : ScriptableObject
{
    public List<DescriptionKeywordSO> keywordSOList;
    private List<string> _keywordList = new();
    private Dictionary<string, Color> _colorDictionary = new();
    private Dictionary<string, string> _descriptionDictionary = new();

    //dont need to add or remove in colorDictionary
    public IReadOnlyDictionary<string, Color> GetColorDictionary() => _colorDictionary;

    [Header("Temp variable")]
    private List<string> _tempedList = new();

    [Header("Need once init")]
    private static bool _inited = false;

    public void Initialize()
    {
        if (_inited) return;

        foreach (var keywordSO in keywordSOList)
        {
            string keyword = keywordSO.keyword;

            _keywordList.Add(keyword);
            _colorDictionary.Add(keyword, keywordSO.textColor);
            _descriptionDictionary.Add(keyword, keywordSO.description);
        }

        _inited = true;
    }

    /// <summary>
    /// Ű���忡 �´� ������ ��ȯ���ش�.
    /// </summary>
    public string GetKeywordDescription(string keyword)
    {
        if (_descriptionDictionary.ContainsKey(keyword))
        {
            return _descriptionDictionary[keyword];
        }
        else
            return "This keyword does not exist";
    }

    ///<summary>
    ///������ ������ Ű���尡 �����ϴ��� �˷��ְ�, �����ϴ� Ű������� ����Ʈ�� ��ȯ����
    ///</summary>
    public bool ExistKeyword(string description)
    {
        foreach (var keywordSO in keywordSOList)
        {
            if (description == "") return false;

            if (description.Contains(keywordSO.keyword)) return true;
        }
        return false;
    }

    /// <summary>
    /// description�� ������ �� description�ȿ� keyword���� �������ش�
    /// </summary>
    public List<string> GetKeywords(string description)
    {
        _tempedList.Clear();

        foreach (var keywordSO in keywordSOList)
        {
            if (description == "") return null;

            if (description.Contains(keywordSO.keyword))
            {
                if (!_tempedList.Contains(keywordSO.keyword))
                    _tempedList.Add(keywordSO.keyword);
            }
        }

        return _tempedList;
    }
}
