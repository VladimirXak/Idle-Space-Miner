using UnityEngine;

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T _instance;

    private static readonly object Lock = new object();

    public static T Instance
    {
        get
        {
            if (Quitting)
            {
                return null;
            }
            lock (Lock)
            {
                if (_instance != null)
                    return _instance;

                var instances = FindObjectsOfType<T>();
                var count = instances.Length;

                if (count > 0)
                {
                    _instance = instances[0];

                    for (var i = 1; i < instances.Length; i++)
                        Destroy(instances[i].gameObject);

                    return _instance;
                }

                return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
            }
        }
    }

    private void Awake()
    {
        if (_instance == null || _instance == this)
            _instance = Instance;
        else
        {
            Destroy(gameObject);
            return;
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool Quitting { get; private set; }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }
}
