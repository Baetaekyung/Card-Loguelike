using System;
using UnityEngine;

namespace CardGame
{
    public class FlameBreath : DamageOnCollision
    {
        private void Start()
        {
            
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                Vector3 mouseWorldPosition = hit.point;
                transform.position = mouseWorldPosition;
                transform.rotation = Quaternion.Euler(270,0,0);
            }
            
        }
    }
}
