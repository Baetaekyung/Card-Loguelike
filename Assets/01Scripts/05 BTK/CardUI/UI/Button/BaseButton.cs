using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {

    }
}
