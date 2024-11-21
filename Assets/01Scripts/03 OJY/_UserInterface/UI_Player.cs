using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class UI_Player : MonoSingleton<UI_Player>
    {
        [SerializeField] private List<TextMeshProUGUI> list;
        public IList<TextMeshProUGUI> GetList => list;
    }
}
