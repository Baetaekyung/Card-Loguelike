using UnityEngine;

namespace CardGame.Weapons
{
    [CreateAssetMenu(menuName = "OJYSO/GenericMeleeSO", order = int.MinValue)]
    public class GenericMeleeSO : BaseMeleeSO
    {
        //public int dd = 111;
        public override void OnEvent(BaseWeapon owner)
        {
            Quaternion rotation = Quaternion.LookRotation(owner.GetTransformEffect.forward, Vector3.up);
            Transform parent = owner.GetTransformEffect; 
            Vector3 position = parent.position;
            Instantiate(prefab, position, rotation, parent);
            //Debug.Log("melee onEvent");
        }
    }
}
