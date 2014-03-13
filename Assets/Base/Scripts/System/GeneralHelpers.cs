// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="GeneralHelpers.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class GeneralHelpers.
/// </summary>
public static class GeneralHelpers
{
    /// <summary>
    /// Calculates the collection positions.
    /// </summary>
    /// <param name="_PreviousPos">The _ previous position.</param>
    /// <param name="_NextPos">The _ next position.</param>
    /// <param name="_CurrentPos">The _ current position.</param>
    /// <param name="_CollectionSize">Size of the _ collection.</param>
    /// <returns>System.Int32.</returns>
    public static int CalculateCollectionPositions(out int _PreviousPos, out int _NextPos, int _CurrentPos,
        int _CollectionSize)
    {
        if ((_CollectionSize - 1) - _CurrentPos < 0)
            _CurrentPos = 0;
        if (_CurrentPos < 0)
            _CurrentPos = _CollectionSize - 1;

        _PreviousPos = _CurrentPos - 1;
        _NextPos = _CurrentPos + 1;

        if (_PreviousPos < 0)
            _PreviousPos = _CollectionSize - 1;
        if ((_CollectionSize - 1) - _NextPos < 0)
            _NextPos = 0;

        return (_CurrentPos);
    }

    /// <summary>
    /// Floats to time string.
    /// </summary>
    /// <param name="_Time">The _ time.</param>
    /// <returns>System.String.</returns>
    public static string FloatToTimeString(this float _Time)
    {
        int lMinutes = Mathf.FloorToInt(_Time / 60.0f);
        int lSeconds = Mathf.FloorToInt(_Time - lMinutes * 60);

        string lFormatedTime = string.Format("{0:0}:{1:00}", lMinutes, lSeconds);

        return lFormatedTime;
    }

    /// <summary>
    /// Lasts the specified _ dic.
    /// </summary>
    /// <param name="_Dic">The _ dic.</param>
    /// <returns>System.String.</returns>
    public static string Last(this Dictionary<string, KeyCode> _Dic)
    {
        string lLast = "";
        string[] lKeys = new string[_Dic.Keys.Count];
        _Dic.Keys.CopyTo(lKeys, 0);

        foreach (string lKey in lKeys)
        {
            lLast = lKey;
        }

        return lLast;
    }

    public static void MoveToBack<T>(this List<T> _List, int _Index)
    {
        T lTmp = _List[_Index];
        _List.RemoveAt(_Index);
        _List.Add(lTmp);
    }

    public static void MoveToFront<T>(this List<T> _List, int _Index)
    {
        T lTmp = _List[_Index];
        _List.RemoveAt(_Index);
        _List.Insert(0, lTmp);
    }
}
