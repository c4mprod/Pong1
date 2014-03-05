using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                if ((m_Instance = (T)GameObject.FindObjectOfType(typeof(T))) == null)
                {
                    GameObject Obj = new GameObject("SingletonBehaviour<" + typeof(T).ToString() + ">");
                    m_Instance = Obj.AddComponent<T>();
                    DontDestroyOnLoad(m_Instance);
                }
            }
            return m_Instance;
        }
    }
}
