using UnityEngine;

namespace CardGame
{
    public class GameExitButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Debug.Log("Game quit");
            Application.Quit();
        }
    }
}
