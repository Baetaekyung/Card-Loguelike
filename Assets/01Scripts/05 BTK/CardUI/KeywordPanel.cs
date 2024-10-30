using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeywordPanel : MonoBehaviour
{
    [SerializeField] private KeywordListSO _keywordList;
    private Image _image;
    private BaseCard _card;
    private string _description; //BaseCard에서 data가져와서 description쓰기
    private string _completeDescription;

    private bool _hasKeyword = false;
    private List<string> _keywords = new List<string>();

    private void InitDescription()
    {
        (_hasKeyword, _keywords) = _keywordList.ExistKeywords(_description);

        if(_hasKeyword)
        {
            for(int i = 0; i < _keywords.Count; i++)
            {
                string description = _keywordList.GetKeywordDescription(_keywords[i]);
                _keywordList.CombineDescription(_completeDescription, description);
            }
        }
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        _card = transform.parent.GetComponent<BaseCard>();
        _keywordList.InitDictionary();

        OffPanel();
    }

    private void OnEnable()
    {
        _card.OnPointerEnterEvent += OnPanel;
        _card.OnPointerDownEvent += OffPanel;
        _card.OnPointerExitEvent += OffPanel;
    }

    private void OnPanel()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 255);
    }

    private void OffPanel()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }

    private void OnDestroy()
    {
        _card.OnPointerEnterEvent -= OnPanel;
        _card.OnPointerDownEvent -= OffPanel;
        _card.OnPointerExitEvent -= OffPanel;
    }
}
