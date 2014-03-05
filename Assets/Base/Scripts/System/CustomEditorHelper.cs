using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CustomEditorHelper
{
    public static void OnInspector(this Goal _Goal)
    {
        EditorGUILayout.EnumPopup("Player", _Goal.m_EPlayer);
    }

    public static void OnGUI(this InputsBindingDatas _InputsBindingDatas)
    {
        GUILayout.Label("Bindable controls", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical("Box");
        {
            GUILayout.Label("Player 1 controls", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(55);
                EditorGUILayout.BeginVertical("Box");
                {
                    KeyCode lKeyPressed = KeyCode.None;

                    foreach (KeyValuePair<string, KeyCode> lEntry in _InputsBindingDatas.m_Player1BindableControls)
                    {
                        if (Event.current.type == EventType.KeyDown)
                            lKeyPressed = Event.current.keyCode;
                        //GuilEntry.v
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
