// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-10-2014
// ***********************************************************************
// <copyright file="PlayerDatas.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class PlayerDatas.
/// </summary>
public class PlayerDatas
{
    /// <summary>
    /// The m_ data score
    /// </summary>
    private float m_DataScore;
    /// <summary>
    /// The m_ racket datas
    /// </summary>
    public SingleRacketDatas m_RacketDatas = null;

    /// <summary>
    /// Gets or sets the m_ score.
    /// </summary>
    /// <value>The m_ score.</value>
    public float m_Score
    {
        get { return this.m_DataScore; }

        set
        {
            if (value >= 0.0f)
                this.m_DataScore = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDatas"/> class.
    /// </summary>
    public PlayerDatas()
    {
        this.m_DataScore = 0;
    }
}
