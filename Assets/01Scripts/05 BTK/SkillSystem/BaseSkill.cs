using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField] protected GameObject _skillEffect;
        [SerializeField] protected Sprite _skillImage;
        public Sprite SkillImage => _skillImage;

        public abstract void ResetSkill(Player owner);
        public abstract void UseSkill(Player owner);
    }
}
