using CardGame.Weapons;
using System.Collections;
using System.Collections.Generic;
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
        private BaseWeapon weapon;
        public BaseWeapon GetCurrentWeapon => weapon;
        public void SetInventory()
        {

        }

        public void Init(Player _player)
        {

        }

        public void Dispose(Player _player)
        {

        }
    }

}
