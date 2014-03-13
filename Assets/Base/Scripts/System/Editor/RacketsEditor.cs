// ***********************************************************************
// Assembly         : Assembly-CSharp-Editor
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="RacketsEditor.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Class RacketsEditor.
/// </summary>
public class RacketsEditor : EditorWindow
{
    /// <summary>
    /// The m_ scroll position
    /// </summary>
    private Vector2 m_ScrollPosition;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [MenuItem("Custom/Rackets Editor")]
    public static void Init()
    {
        EditorWindow lWindow = EditorWindow.GetWindow<RacketsEditor>("Rackets Editor", true);

        lWindow.minSize = new Vector2(500, 500);
    }

    /// <summary>
    /// Called when [GUI].
    /// </summary>
    void OnGUI()
    {
        this.m_ScrollPosition = EditorGUILayout.BeginScrollView(this.m_ScrollPosition, false, true);

        EditorGUILayout.Separator();
        GlobalDatasModel.Instance.m_RacketsData.OnGUI();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Save", GUILayout.Width(200f)))
            {
                GlobalDatasModel.Instance.m_RacketsData.Save<RacketsDatas>(RacketsDatas.EditorPath);
            }
        }
        EditorGUILayout.EndHorizontal();
        /**
         ** A save button which is going to save our custom bindable asset.
         ** It will be loaded at game start.
         **/
        EditorGUILayout.EndScrollView();
    }
}
