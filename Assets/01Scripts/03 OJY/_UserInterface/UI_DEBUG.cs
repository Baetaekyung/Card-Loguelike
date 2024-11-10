using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class UI_DEBUG : MonoSingleton<UI_DEBUG>
    {
        [SerializeField] private List<TextMeshProUGUI> list = new();
        public List<TextMeshProUGUI> GetList => list;
    }

}
