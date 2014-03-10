using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RacketsEditor : EditorWindow
{
    private Vector2 m_ScrollPosition;

    [MenuItem("Custom/Rackets Editor")]
    public static void Init()
    {
        EditorWindow lWindow = EditorWindow.GetWindow<RacketsEditor>("Rackets Editor", true);

        lWindow.minSize = new Vector2(500, 500);
    }

    void OnGUI()
    {
        this.m_ScrollPosition = EditorGUILayout.BeginScrollView(this.m_ScrollPosition, false, true);

        EditorGUILayout.Separator();
        GlobalDatas.Instance.m_RacketsData.OnGUI();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Save", GUILayout.Width(200f)))
            {
                GlobalDatas.Instance.m_RacketsData.Save<RacketsDatas>(RacketsDatas.Path);
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
