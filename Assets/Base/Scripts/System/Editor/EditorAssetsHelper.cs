// ***********************************************************************
// Assembly         : Assembly-CSharp-Editor
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="EditorAssetsHelper.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Class EditorAssetsHelper.
/// </summary>
public static class EditorAssetsHelper 
{
    /// <summary>
    /// Saves the specified _ asset.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Asset">The _ asset.</param>
    /// <param name="_Path">The _ path.</param>
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
