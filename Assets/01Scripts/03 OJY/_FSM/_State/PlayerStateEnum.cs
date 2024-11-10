using UnityEngine;

namespace CardGame
{
    public class PlayerStateEnum
    {
        public enum Movement
        {
            None,
            Idle,
            Dash
        }
        public enum Combat
        {
            None,
            NormalAttack,
            SpecialAttack
        }
    }
}
