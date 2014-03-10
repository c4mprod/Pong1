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

    #region "Rackets"

    public static void OnGUI(this RacketsDatas _RacketsDatas)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical("Box");
        {
            //GUILayout.Label(_Message, EditorStyles.boldLabel);
            int i = -1;

            while (++i < _RacketsDatas.m_RacketsList.Count)
            {
                GUILayout.Label("Racket " + (i + 1), EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("Box");
                {
                    _RacketsDatas.m_RacketsList[i].OnGUI();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add Racket", GUILayout.Width(200f)))
                {
                    _RacketsDatas.m_RacketsList.Add(new SingleRacketDatas());
                }
                if (GUILayout.Button("Remove Racket", GUILayout.Width(200f)))
                {
                    _RacketsDatas.m_RacketsList.RemoveAt(_RacketsDatas.m_RacketsList.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    public static void OnGUI(this SingleRacketDatas _RacketDatas)
    {
        if (_RacketDatas == null)
            return;
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Racket Name : ");
            _RacketDatas.m_Name = EditorGUILayout.TextField(_RacketDatas.m_Name);
            GUILayout.Space(5);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Racket Sprite : ");
            _RacketDatas.m_Sprite = (Sprite)EditorGUILayout.ObjectField((Object)_RacketDatas.m_Sprite, typeof(Sprite), false);
            GUILayout.Space(5);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Racket Speed : ");
            _RacketDatas.m_Speed = EditorGUILayout.FloatField(_RacketDatas.m_Speed);
            GUILayout.Space(5);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Racket Width : ");
            _RacketDatas.m_Width = EditorGUILayout.FloatField(_RacketDatas.m_Width);
        }
        EditorGUILayout.EndHorizontal();
    }

    #endregion
}
