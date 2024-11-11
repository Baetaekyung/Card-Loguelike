using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class UI_Inv : MonoSingleton<UI_Inv>
    {
        [SerializeField] private List<TextMeshProUGUI> list = new();
        public List<TextMeshProUGUI> GetList => list;
    }
}
