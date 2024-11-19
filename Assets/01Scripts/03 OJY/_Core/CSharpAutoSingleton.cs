using UnityEngine;

public static class CSharpAutoSingleton<T> where T : class, new()
{
    public static T instance = new();
}
