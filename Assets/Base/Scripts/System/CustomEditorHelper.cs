using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CustomEditorHelper 
{
    public static void OnInspector(this Goal _Goal)
    {
        EditorGUILayout.EnumPopup("Player", _Goal.m_EPlayer);
    }
}
