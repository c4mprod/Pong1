// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="GenericCustomAsset.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class GenericCustomAsset.
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericCustomAsset<T> : ScriptableObject
    where T : ScriptableObject
{
    /// <summary>
    /// Loads the specified _ editor path.
    /// </summary>
    /// <param name="_EditorPath">The _ editor path.</param>
    /// <param name="_PlayerPAth">The _ player p ath.</param>
    /// <returns>T.</returns>
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
