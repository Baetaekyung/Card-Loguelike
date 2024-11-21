using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    [MonoSingletonUsage(MonoSingletonFlags.SingletonPreset)]
    public class UI_Wave : MonoSingleton<UI_Wave>
    {
        [SerializeField] private List<TextMeshProUGUI> list = new();
        public List<TextMeshProUGUI> GetList => list;
        private void Start()
        {
            gameObject.SetActive(false);
        }
    }

}
