using System;
using UnityEngine;

namespace CardGame
{
    public class DondestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
