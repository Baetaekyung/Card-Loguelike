using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public interface IPlayerComponent
    {
        void Init(Player _player);
        /// UnSubscribe all events here
        void Dispose(Player _player);
    }

}
