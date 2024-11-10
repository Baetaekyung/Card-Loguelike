using CardGame.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        //private readonly List<CardSO> listCard = new();
        //public IReadOnlyList<CardSO> GetInventory => listCard;
        //public void AddInventory(CardSO instance)
        //{
        //    listCard.Add(instance);
        //}

        private int currentWeaponIndex;
        [SerializeField] private List<BaseWeapon> weapons;
        public BaseWeapon GetCurrentWeapon => weapons[currentWeaponIndex];

        public void Init(Player _player)
        {
            weapons = GetComponentsInChildren<BaseWeapon>(true).ToList();
        }

        public void Dispose(Player _player)
        {

        }
    }

}
