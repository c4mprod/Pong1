// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-10-2014
// ***********************************************************************
// <copyright file="DontDestroy.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// Class DontDestroy.
/// </summary>
public class DontDestroy : MonoBehaviour
{
    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
