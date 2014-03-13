// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-07-2014
// ***********************************************************************
// <copyright file="SingletonBehaviour.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// Class SingletonBehaviour.
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    /// <summary>
    /// The data instance
    /// </summary>
    private static T DataInstance = null;
    /// <summary>
    /// The destroyed
    /// </summary>
    private static bool Destroyed = false;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
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

    /// <summary>
    /// Called when [destroy].
    /// </summary>
    void OnDestroy()
    {
        Destroyed = true;
    }
}
