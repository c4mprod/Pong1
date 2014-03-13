// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="InputsDatas.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class InputsDatas.
/// </summary>
public class InputsDatas : GenericCustomAsset<InputsDatas>
{
    /// <summary>
    /// The play path
    /// </summary>
    public static readonly string PlayPath = "Prefs/inputsBinding";
    /// <summary>
    /// The editor path
    /// </summary>
    public static readonly string EditorPath = "Assets/Base/Resources/Prefs/inputsBinding.asset";

    /// <summary>
    /// The m_ player1 bindable controls
    /// </summary>
    public Dictionary<string, KeyCode> m_Player1BindableControls = new Dictionary<string,KeyCode>();
    /// <summary>
    /// The m_ player2 bindable controls
    /// </summary>
    public Dictionary<string, KeyCode> m_Player2BindableControls = new Dictionary<string, KeyCode>();
    /// <summary>
    /// The m_ general controls
    /// </summary>
    public Dictionary<string, KeyCode> m_GeneralControls = new Dictionary<string, KeyCode>();

    /// <summary>
    /// The m_ mouse controls
    /// </summary>
    public Dictionary<string, int> m_MouseControls = new Dictionary<string, int>();
}
