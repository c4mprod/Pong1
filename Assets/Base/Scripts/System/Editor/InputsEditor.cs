// ***********************************************************************
// Assembly         : Assembly-CSharp-Editor
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="InputsEditor.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Class InputsEditor.
/// </summary>
public class InputsEditor : EditorWindow
{
    /// <summary>
    /// Class ToogleHelper.
    /// </summary>
    public class ToogleHelper
    {
        /// <summary>
        /// The m_ toogle identifier dictionary
        /// </summary>
        public Dictionary<int, bool> m_ToogleIdDictionary = new Dictionary<int, bool>();
    }

    /// <summary>
    /// The m_ toogle helper
    /// </summary>
    private ToogleHelper m_ToogleHelper = new ToogleHelper();
    /// <summary>
    /// The m_ scroll position
    /// </summary>
    private Vector2 m_ScrollPosition;
    /// <summary>
    /// The m_ toogle position
    /// </summary>
    private int m_TooglePosition;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [MenuItem("Custom/Inputs Editor")]
    public static void Init()
    {
        EditorWindow lWindow = EditorWindow.GetWindow<InputsEditor>("Inputs Editor", true);

        lWindow.minSize = new Vector2(500, 500);
    }

    /// <summary>
    /// Called when [GUI].
    /// </summary>
    void OnGUI()
    {
        this.m_TooglePosition = 0;
        this.m_ScrollPosition = EditorGUILayout.BeginScrollView(this.m_ScrollPosition, false, true);

        /**
         ** We are calling OnGUI extension class (see CustomEditorHelper) for each bindable controls dictionary.
         ** ToogleHelper remember selected control and we can associate keyboard event to simulate a bindable key button.
         **/

        GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 1 controls");
        GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 2 controls");
        GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "General controls");

        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        {
            /**
             ** A save button which is going to save our custom bindable asset.
             ** It will be loaded at game start.
             **/
            if (GUILayout.Button("Save", GUILayout.Width(200f)))
            {
                GlobalDatasModel.Instance.m_InputsBinding.Save<InputsDatas>(InputsDatas.EditorPath);
            }

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }
}
