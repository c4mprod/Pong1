using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CustomEditorHelper
{
    public static void OnGUI(this Dictionary<string, KeyCode> _InputBindingDictionary, InputsEditor.ToogleHelper _ToogleHelper,
        ref int _TooglePosition, string _Message)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical("Box");
        {
            GUILayout.Label(_Message, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(55);
                EditorGUILayout.BeginVertical("Box");
                {
                    KeyCode lKeyPressed = KeyCode.None;

                    string[] lKeys = new string[_InputBindingDictionary.Keys.Count];
                    _InputBindingDictionary.Keys.CopyTo(lKeys, 0);

                    foreach (string lKey in lKeys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Label(lKey);

                            if (!_ToogleHelper.m_ToogleIdDictionary.ContainsKey(_TooglePosition))
                                _ToogleHelper.m_ToogleIdDictionary[_TooglePosition] = false;
                            if ((_ToogleHelper.m_ToogleIdDictionary[_TooglePosition] = GUILayout.Toggle(_ToogleHelper.m_ToogleIdDictionary[_TooglePosition], "" + _InputBindingDictionary[lKey])) == true)
                            {
                                int[] lToogleKeys = new int[_ToogleHelper.m_ToogleIdDictionary.Keys.Count];
                                _ToogleHelper.m_ToogleIdDictionary.Keys.CopyTo(lToogleKeys, 0);

                                foreach (int lToogleKey in lToogleKeys)
                                {
                                    if (lToogleKey != _TooglePosition)
                                        _ToogleHelper.m_ToogleIdDictionary[lToogleKey] = false;
                                }
                                if (Event.current.type == EventType.KeyDown)
                                {
                                    lKeyPressed = Event.current.keyCode;
                                    if (lKeyPressed != KeyCode.None)
                                        _InputBindingDictionary[lKey] = lKeyPressed;
                                }
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        ++_TooglePosition;
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
}
