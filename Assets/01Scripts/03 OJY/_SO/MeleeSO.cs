using UnityEngine;

namespace CardGame.Weapons
{
    [CreateAssetMenu(menuName = "OJYSO/MeleeSO", order = int.MinValue)]
    public class MeleeSO : BaseMeleeSO
    {
        public int kk = 111;
        public override void OnEvent(BaseWeapon owner)
        {
            Instantiate(prefab, owner.transform.position, Quaternion.identity, null);
            Debug.Log("onEvent" + kk);
        }
    }
}
