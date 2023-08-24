using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (this is T)
        {
            if (_instance == null || _instance == this)
            {
                _instance = this as T;
            }
            else
            {
                Debug.LogWarning("An instance of type [" + typeof(T) + "] already exists, and you are trying to create " +
                                 "another one on Object [" + name + "].", _instance);
            }
        }
        else
        {
            Debug.LogError("MonoBehaviourSingleton has been initialised for type [" + GetType() +
                           "], but the instance is for type [" + typeof(T) + "].");
        }
    }
}