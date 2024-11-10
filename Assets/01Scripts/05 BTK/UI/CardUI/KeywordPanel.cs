using CardGame;
using CustomUtils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeywordPanel : MonoBehaviour
{
    [Header("Serialize variables")]
    [SerializeField] private KeywordListSO _keywordList;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private UseableCard _card;

    private CanvasGroup _canvasGroup;
    private string _description;
    private string _completeDescription = "";

    private List<KeywordEnum> _keywords = new List<KeywordEnum>();
    public List<KeywordEnum> Keywords => _keywords;

    private bool _hasKeyword = false;

    private void Awake()
    {
        _completeDescription = "";
        _canvasGroup = GetComponent<CanvasGroup>();
        _keywordList.Initialize();
        _description = _card.CardData.cardInfo.cardDescription;
    }

    //설명 초기화
    private void InitDescription()
    {
        _hasKeyword = _keywordList.ExistKeyword(_description);

        if (!_hasKeyword) return;

        _keywords = _card.keywords;

        for (int i = 0; i < _keywords.Count; i++)
        {
            string description = _keywordList.GetKeywordDescription(_keywords[i]);

            _keywordList.GetColorDictionary()
                .TryGetValue(_keywords[i], out Color keywordColor);

            description = TextUtility.GivePointColor(
                            Enum.GetName(typeof(KeywordEnum), (int)_keywords[i]),
                            description,
                            keywordColor != null ? keywordColor : Color.white);

            _completeDescription =
                TextUtility.CombineTextWithEnter(_completeDescription, description);
        }

        _descriptionText.text = _completeDescription;
    }

    private void Start()
    {
        InitDescription();
        OffPanel();
    }

    void OnEnable()
    {
        _card.OnPointerEnterEvent += OnPanel;
        _card.OnPointerDownEvent += OffPanel;
        _card.OnPointerExitEvent += OffPanel;
    }

    private void OnPanel()
    {
        _canvasGroup.alpha = 1;
    }

    private void OffPanel()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        _card.OnPointerEnterEvent -= OnPanel;
        _card.OnPointerDownEvent -= OffPanel;
        _card.OnPointerExitEvent -= OffPanel;
    }
}
