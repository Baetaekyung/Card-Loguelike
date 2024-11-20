using System;
using CardGame.Players;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class FireballSkill : BaseSkill
    {
        

       [SerializeField]private ActionData _data;

       private void Start()
       {
       }

       public override void ResetSkill()
       {
           
       }

       public override void UseSkill(Player owner)
        {
            
        }
    }
}
