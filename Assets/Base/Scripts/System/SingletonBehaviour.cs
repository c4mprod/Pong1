using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                if ((_instance = (T)GameObject.FindObjectOfType(typeof(T))) == null)
                {
                    GameObject o = new GameObject("SingletonBehaviour<" + typeof(T).ToString() + ">");
                    _instance = o.AddComponent<T>();
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }
}
