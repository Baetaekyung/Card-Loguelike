using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class InventoryUI : MonoSingleton<InventoryUI>
    {
        [SerializeField] private List<TextMeshProUGUI> list = new();
        public List<TextMeshProUGUI> GetList => list;
    }

}
