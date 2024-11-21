using System;
using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public class CreateSkillObject : BaseSkill
    {
        [SerializeField] private GameObject SkillObject;
        

        protected override void UseSkill(Player owner)
        {
            Instantiate(SkillObject, owner.transform.GetChild(0).transform.position, 
                owner.transform.GetChild(0).rotation);
        }
        
        
    }
}
