using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T DataInstance = null;

    public static T Instance
    {
        get
        {
            if ((DataInstance = GameObject.FindObjectOfType<T>()) == null)
            {
                DataInstance = new GameObject("SingletonBehaviour<" + typeof(T).ToString() + ">").AddComponent<T>();
                DontDestroyOnLoad(DataInstance);
            }
            return DataInstance;
        }
    }

    void OnDisable()
    {
        if (DataInstance)
        {
            Destroy(DataInstance);
            DataInstance = null;
        }
    }
}
