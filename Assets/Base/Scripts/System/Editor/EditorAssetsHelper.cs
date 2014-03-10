using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class EditorAssetsHelper 
{
    public static void Save<T>(this GenericCustomAsset<T> _Asset, string _Path)
        where T : ScriptableObject
    {
        EditorUtility.SetDirty(_Asset);
        GenericCustomAsset<T> lLoadTest = Resources.LoadAssetAtPath<GenericCustomAsset<T>>(_Path);

        if (lLoadTest == null)
            AssetDatabase.CreateAsset(_Asset, _Path);
        else
            AssetDatabase.SaveAssets();
    }
}
