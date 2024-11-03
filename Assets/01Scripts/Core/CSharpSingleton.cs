using System;
using System.Reflection;
using UnityEngine;

namespace CardGame
{
    public static class CSharpSingleton<T> where T : class, new()
    {
        private static T _instance = Initialize();
        public static T Instance
        {
            get
            {
                //if (_instance == null) _instance = Initialize();
                return _instance;
            }
            //set => _instance = value;
        } 
        private static T Initialize()
        {
            return new T();
        }
    }
}
