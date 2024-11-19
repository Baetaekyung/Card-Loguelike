using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    [MonoSingletonUsage(MonoSingletonFlags.SingletonPreset)]
    public class UI_DEBUG : MonoSingleton<UI_DEBUG>
    {
        [SerializeField] private List<TextMeshProUGUI> list = new();
        public bool val;
        public List<TextMeshProUGUI> GetList => list;
        private void Start()
        {
            gameObject.SetActive(val);
        }
    }

}
