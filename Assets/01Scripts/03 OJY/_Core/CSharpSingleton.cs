using System;
using System.Reflection;
using UnityEngine;

namespace CardGame
{
    public abstract class CSharpSingleton<T> where T : class
    {
        private static T _instance = null;
        public static T Instance => _instance;
        protected CSharpSingleton()
        {
            _instance = this as T;
        }
    }
}
