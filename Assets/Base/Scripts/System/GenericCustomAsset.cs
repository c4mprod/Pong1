using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GenericCustomAsset<T> : ScriptableObject
    where T : ScriptableObject
{
    public static T Load(string _Path)
    {
        T lTmp = null;

        if ((lTmp = Resources.LoadAssetAtPath<T>(_Path)) == null)
        {
            Debug.Log("NOUL");
            return (ScriptableObject.CreateInstance<T>());
        }
        return lTmp;
    }
}
