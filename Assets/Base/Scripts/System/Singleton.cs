// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-06-2014
// ***********************************************************************
// <copyright file="Singleton.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// Class Singleton.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T>
    where T : new()
{
    /// <summary>
    /// The data instance
    /// </summary>
    private static T DataInstance;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
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
