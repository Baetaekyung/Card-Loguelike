using CardGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordList", menuName = "SO/Keyword/KeywordList")]
public class KeywordListSO : ScriptableObject
{
    public List<DescriptionKeywordSO> keywordSOList;
    private List<KeywordEnum> _keywordList = new();
    private Dictionary<KeywordEnum, Color> _colorDictionary = new();
    private Dictionary<KeywordEnum, string> _descriptionDictionary = new();

    public IReadOnlyDictionary<KeywordEnum, Color> GetColorDictionary() => _colorDictionary;

    private List<KeywordEnum> _tempedList = new();

    private static bool _inited = false;

    public void Initialize()
    {
        if (_inited) return;

        _keywordList.Clear();
        _colorDictionary.Clear();
        _descriptionDictionary.Clear();

        foreach (var keywordSO in keywordSOList)
        {
            KeywordEnum keywordEnum = keywordSO.keyword;

            _keywordList.Add(keywordEnum);
            _colorDictionary.Add(keywordEnum, keywordSO.textColor);
            _descriptionDictionary.Add(keywordEnum, keywordSO.description);
        }

        _inited = true;
    }

    /// <summary>
    /// 키워드에 맞는 설명을 반환해준다.
    /// </summary>
    public string GetKeywordDescription(KeywordEnum keywordEnum)
    {
        if (_descriptionDictionary.ContainsKey(keywordEnum))
        {
            return _descriptionDictionary[keywordEnum];
        }
        else
            return "This keyword does not exist";
    }

    ///<summary>
    ///설명을 넣으면 키워드가 존재하는지 알려주고, 존재하는 키워드들을 리스트로 반환해줌
    ///</summary>
    public bool ExistKeyword(string description)
    {
        foreach (var keywordSO in keywordSOList)
        {
            if (description == "") return false;

            if (description.Contains(Enum.GetName(typeof(KeywordEnum), (int)keywordSO.keyword)))
                return true;
        }
        return false;
    }

    /// <summary>
    /// description을 넣으면 그 description안에 keyword들을 리턴해준다
    /// </summary>
    public List<KeywordEnum> GetKeywords(string description)
    {
        _tempedList.Clear();

        foreach (var keywordSO in keywordSOList)
        {
            if (description == "") return null;

            string keyword = Enum.GetName(typeof(KeywordEnum), (int)keywordSO.keyword);

            if (description.Contains(keyword))
            {
                if (!_tempedList.Contains(keywordSO.keyword))
                    _tempedList.Add(keywordSO.keyword);
            }
        }

        return _tempedList;
    }
}
