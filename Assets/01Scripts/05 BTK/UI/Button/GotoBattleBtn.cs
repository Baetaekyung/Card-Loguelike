using UnityEngine;

namespace CardGame
{
    public class GotoBattleBtn : BaseButton
    {
        protected override void OnClick()
        {
            SceneManagerEx.Instance.ChangeScene(SceneEnum.Scene3D);
        }
    }
}
