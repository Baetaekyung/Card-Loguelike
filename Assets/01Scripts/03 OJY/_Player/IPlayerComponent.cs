using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public interface IPlayerComponent
    {
        void Init(Player _player);
        /// <summary>
        /// UnSubscribe all events here
        /// </summary>
        void Dispose(Player _player);
    }

}
