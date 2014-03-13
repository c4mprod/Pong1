// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="RacketsDatas.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class RacketsDatas.
/// </summary>
[System.Serializable]
public class RacketsDatas : GenericCustomAsset<RacketsDatas>
{
    /// <summary>
    /// The play path
    /// </summary>
    public static readonly string PlayPath = "Rackets/racketsDatas";
    /// <summary>
    /// The editor path
    /// </summary>
    public static readonly string EditorPath = "Assets/Base/Resources/Rackets/racketsDatas.asset";

    /// <summary>
    /// The m_ rackets list
    /// </summary>
    public List<SingleRacketDatas> m_RacketsList = new List<SingleRacketDatas>();
}
