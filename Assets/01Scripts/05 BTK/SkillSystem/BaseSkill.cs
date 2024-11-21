using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField] protected GameObject _skillEffect;
        [SerializeField] protected Sprite _skillImage;
        public Sprite SkillImage => _skillImage;
        private float currentDelayTime = 0;
        protected bool CanAttack => currentDelayTime < Time.time;
        public void AWDlpawdakdpadoawkakwodpkdopwkdakWOdkowaijdiwadjiadwajdiwadwioajdaiodjawdjioajdiawdajddjnjdkjlkjngjjkjkgjkkbkcxkjkxfjkggjggkggkggjkggkgkggkgkgjgkgjxzlxkmbkcmkmvklcjbxklxjbjkjcixjbpokrjriyjeiitjgifdfgdkgjkdfglndhdfkd()
        {
            currentDelayTime = 0;
        }
        public bool TryUseSkill(Player owner)
        {
            bool canAttack = CanAttack;
            UI_DEBUG.Instance.GetList[4].text = currentDelayTime.ToString() + " " + Time.time;
            if (canAttack)
            {
                float delay = 1;
                currentDelayTime = delay + Time.time;
                UseSkill(owner);
            }
            return canAttack;
        }
        protected abstract void UseSkill(Player owner);
    }
}
