using UnityEngine;
using System.Collections;

public class Singleton<T>
    where T : new()
{
    private static T DataInstance;

    public static T Instance
    {
        get
        {
            if (DataInstance == null)
                DataInstance = new T();
            return DataInstance;
        }
    }
}
