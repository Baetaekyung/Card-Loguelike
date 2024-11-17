using UnityEngine;

namespace CardGame.FSM
{
    public class PlayerStateEnum
    {
        public enum Movement
        {
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
