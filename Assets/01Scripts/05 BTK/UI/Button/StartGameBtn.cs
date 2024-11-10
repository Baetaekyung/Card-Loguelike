using UnityEngine;

public class StartGameBtn : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        Debug.Log("Game Start!!");
    }
}
