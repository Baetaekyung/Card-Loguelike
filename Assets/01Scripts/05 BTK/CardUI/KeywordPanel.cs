using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CustomUtils;

public class KeywordPanel : MonoBehaviour
{
    [Header("Serialize variables")]
    [SerializeField] private KeywordListSO _keywordList;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private BaseCard _card;

    private CanvasGroup _canvasGroup;
    public string _description; //BaseCard에서 data가져와서 description쓰기 현재는 Test용으로 public
    private string _completeDescription = "";

    private List<string> _keywords = new List<string>();

    private bool _hasKeyword = false;

    private void Awake()
    {
        _completeDescription = "";
        _canvasGroup = GetComponent<CanvasGroup>();
        _keywordList.Initialize();
    }

    //설명 초기화
    private void InitDescription()
    {
        _hasKeyword = _keywordList.ExistKeyword(_description);

        if (!_hasKeyword) return;

        _keywords = _keywordList.GetKeywords(_description);

        for (int i = 0; i < _keywords.Count; i++)
        {
            string description = _keywordList.GetKeywordDescription(_keywords[i]);

            _keywordList.GetColorDictionary()
                .TryGetValue(_keywords[i], out Color keywordColor);

            description = TextUtility.GivePointColor(
                            _keywords[i],
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
