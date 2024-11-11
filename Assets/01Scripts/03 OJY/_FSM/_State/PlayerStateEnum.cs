using UnityEngine;

namespace CardGame.FSM
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
