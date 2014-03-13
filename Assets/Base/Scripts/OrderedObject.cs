// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-13-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-13-2014
// ***********************************************************************
// <copyright file="OrderedObject.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class OrderedObject.
/// </summary>
public class OrderedObject : MonoBehaviour 
{
    /// <summary>
    /// The m_ position
    /// </summary>
    public int m_Position = 0;
    /// <summary>
    /// The m_ name text mesh
    /// </summary>
    public GameObject m_NameTextMesh;
    /// <summary>
    /// The m_ width text mesh
    /// </summary>
    public GameObject m_WidthTextMesh;
    /// <summary>
    /// The m_ speed text mesh
    /// </summary>
    public GameObject m_SpeedTextMesh;

    /// <summary>
    /// Loads the racket datas.
    /// </summary>
    /// <param name="_RacketDatas">The _ racket datas.</param>
    public void LoadRacketDatas(SingleRacketDatas _RacketDatas)
    {
        this.GetComponent<SpriteRenderer>().sprite = _RacketDatas.m_Sprite;
        this.m_NameTextMesh.GetComponent<TextMesh>().text = "Racket Name : " + _RacketDatas.m_Name;
        this.m_WidthTextMesh.GetComponent<TextMesh>().text = "Racket Width : " + _RacketDatas.m_Width;
        this.m_SpeedTextMesh.GetComponent<TextMesh>().text = "Racket Speed : " + _RacketDatas.m_Speed;
    }
}
