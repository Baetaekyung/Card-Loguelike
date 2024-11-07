using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO//Stat/StatSO")]
    public class StatSO : ScriptableObject
    {
        public Action<float> OnValueChanged; // 이 스탯이 변경되었을 때 재적용해줘야 하는 내용을 알려주기 위한 이벤트

        public string statName;
        public string description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _baseValue, _minValue, _maxValue;//기본값, 최소와 최댓값
        private Dictionary<string, float> _modifiers = new Dictionary<string, float>();

        private float _modifyValue = 0; // modify를 얼마나 해야하는지에 대한 값
        public Sprite Icon => _icon;

        //프로퍼티로 빼줘서 제한변역 내로 값을 고정함, 기본값과 변해야 하는 값을 합쳐서 최종 값을 산출함 *제한변역 : 값 변경시 해당 값이 특정 범위 안에 있어야 할때 범위를 의미함.
        public float Value => Mathf.Clamp(_baseValue + _modifyValue, _minValue, _maxValue);

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = Mathf.Clamp(value, _minValue, _maxValue);//제한변역에 맞춰 값 넣어주기
                OnValueChanged?.Invoke(Value);
            }
        }

        public void AddModifier(string key, float value)
        {
            if (_modifiers.ContainsKey(key)) return;

            _modifyValue += value;
            _modifiers.Add(key, value);
            OnValueChanged?.Invoke(Value);
        }
        public void RemoveModifier(string key)
        {
            if (_modifiers.TryGetValue(key, out float value))
            {
                _modifyValue -= value;
                _modifiers.Remove(key);
                OnValueChanged?.Invoke(Value);
            }
        }
    }
}
