using UnityEngine;
using System.Reflection;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static bool IsShuttingDown { get; private set; }
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance is null)
            {
                if (IsShuttingDown) return null;
                _instance = Initialize();
            }
            return _instance;
        }
    }
    private static T Initialize()
    {
        //CreateInstance
        GameObject gameObject = new(name:"Runtime_Singleton_" + typeof(T).Name);
        T result = gameObject.AddComponent<T>();
        return result;
    }
    protected virtual void Awake()
    {
        var attribute = typeof(T).GetCustomAttribute<MonoSingletonUsageAttribute>();
        var flag = attribute != null ? attribute.Flag : MonoSingletonFlags.None;
        if (flag.HasFlag(MonoSingletonFlags.DontDestroyOnLoad)) DontDestroyOnLoad(gameObject);

        if (_instance is not null)
        {
            Debug.LogError("[Unity]twoSingletons_" + typeof(T).Name);
            Destroy(gameObject);
            return;
        }
        print("-AwakeInit-" + typeof(T).Name);
        _instance = this as T;
    }
    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        IsShuttingDown = true;
    }

}