using UnityEngine;
using System.Collections.Generic;

public class GenericCustomAsset<T> : ScriptableObject
    where T : ScriptableObject
{
    public static T Load(string _EditorPath, string _PlayerPAth)
    {
        T lTmp = null;

        if (Application.isEditor)
        {
            if ((lTmp = Resources.LoadAssetAtPath<T>(_EditorPath)) == null)
            {
                Debug.LogError("Load in editor Error");
                return (ScriptableObject.CreateInstance<T>());
            }
        }
        else
        {
            if ((lTmp = (T)Resources.Load(_PlayerPAth, typeof(T))) == null)
            {
                Debug.LogError("Load in play Error");
                return (ScriptableObject.CreateInstance<T>());
            }
        }

        return lTmp;
    }
}
