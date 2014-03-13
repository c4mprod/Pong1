// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-07-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-07-2014
// ***********************************************************************
// <copyright file="GameObjectPool.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class GameObjectPool.
/// </summary>
public class GameObjectPool : GenericPool<GameObject> 
{
    /// <summary>
    /// Generates the specified _ generation number.
    /// </summary>
    /// <param name="_GenerationNumber">The _ generation number.</param>
    /// <param name="_ObjectToInstantiate">The _ object to instantiate.</param>
    public override void Generate(int _GenerationNumber, GameObject _ObjectToInstantiate)
    {
        int i = -1;

        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            GameObject lObject = (GameObject)GameObject.Instantiate(_ObjectToInstantiate);

            lObject.SetActive(false);
            this.m_ObjectsList.Add(lObject);
        }
    }

    /// <summary>
    /// Generates the specified _ generation number.
    /// </summary>
    /// <param name="_GenerationNumber">The _ generation number.</param>
    /// <param name="_ObjectToInstantiate">The _ object to instantiate.</param>
    /// <param name="_Parent">The _ parent.</param>
    public void Generate(int _GenerationNumber, GameObject _ObjectToInstantiate,
        Transform _Parent = null)
    {  
        int i = -1;
        
        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            GameObject lObject = (GameObject)GameObject.Instantiate(_ObjectToInstantiate);

            lObject.SetActive(false);
            if (_Parent != null)
                lObject.transform.parent = _Parent;
            this.m_ObjectsList.Add(lObject);
        }
    }

    /// <summary>
    /// Puts the object.
    /// </summary>
    /// <param name="_Object">The _ object.</param>
    public override void PutObject(GameObject _Object)
    {
        _Object.SetActive(false);

        try
        {
            base.PutObject(_Object);
        }
        catch (GenericPoolException _Exception)
        {
            throw _Exception;
        }
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns>GameObject.</returns>
    public override GameObject GetObject()
    {
        try
        {
            GameObject lObject = (GameObject)base.GetObject();

            lObject.SetActive(true);
            return lObject;
        }
        catch (GenericPoolException _Exception)
        {
            throw _Exception;
        }
    }
}
