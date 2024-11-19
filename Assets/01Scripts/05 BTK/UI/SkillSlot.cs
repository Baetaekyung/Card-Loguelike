using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class SkillSlot : MonoBehaviour
    {
        public Image skillSprite;
        public CanvasGroup cG;

        public void SetSkillImage(Sprite skillSprite)
        {
            this.skillSprite.sprite = skillSprite;
        }
    }
}
