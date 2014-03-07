using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T DataInstance = null;
    private static bool Destroyed = false;

    public static T Instance
    {
        get
        {
            if (!Destroyed && DataInstance == null
                && (DataInstance = GameObject.FindObjectOfType<T>()) == null)
            {
                DataInstance = new GameObject("SingletonBehaviour<" + typeof(T).ToString() + ">").AddComponent<T>();
                DontDestroyOnLoad(DataInstance.gameObject);
            }
            return DataInstance;
        }
    }

    void OnDestroy()
    {
        Destroyed = true;
    }
}
