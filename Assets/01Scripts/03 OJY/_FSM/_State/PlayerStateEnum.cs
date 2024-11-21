using UnityEngine;

namespace CardGame.FSM
{
    public class PlayerStateEnum
    {
        public enum Movement
        {
            Idle,
            Running,
            Roll,
            Attack,
            Death
        }
        public enum Combat
        {
            None,
            NormalAttack,
            SpecialAttack
        }
    }
}
