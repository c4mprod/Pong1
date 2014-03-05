using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class InputsEditor : EditorWindow
{
    #region "Events"

    private event GameManager.CustomEventHandler m_ModifyInputEvent;

    #endregion

    private Vector2 m_ScrollPosition;
    private GameManager m_GameManager = null;

    [MenuItem("Custom/Inputs Editor")]
    public static void Init()
    {
        EditorWindow lWindow = EditorWindow.GetWindow<InputsEditor>("Inputs Editor", true);

        lWindow.minSize = new Vector2(500, 500);
    }

    void OnEnable()
    {
        this.m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        this.m_ModifyInputEvent += this.m_GameManager.OnModifyInput;
    }

    void OnDisable()
    {
        this.m_ModifyInputEvent -= this.m_GameManager.OnModifyInput;
    }

    void OnGUI()
    {
        this.m_ScrollPosition = EditorGUILayout.BeginScrollView(this.m_ScrollPosition, false, true);
        //this._datas.levelsHolder.OnGui();

        //EditorGUILayout.Separator();
        //EditorGUILayout.BeginHorizontal();
        //{
        //    if (GUILayout.Button("Save", GUILayout.Width(200f)))
        //    {
        //        //    LevelsHolder tmp = this._datas.levelsHolder;
        //        //    this._datas.levelsHolder.apply();
        //        //    this._sr = this._factory.Create(SerializerType.XmlSerializer);

        //        //    this._sr.Serialize<LevelsHolder>(ref tmp, Application.dataPath + GlobalDatas.levelsEditorPrefsPath);
        //    }
        //    if (GUILayout.Button("Reset", GUILayout.Width(200f)))
        //    {
        //        //LevelsHolder levelsHolder = this._datas.levelsHolder;

        //        //this._datas.loadData<LevelsHolder>(ref levelsHolder, GlobalDatas.levelsEditorPrefsPath);
        //    }

        //}
        //EditorGUILayout.EndHorizontal();
        //EditorGUILayout.EndScrollView();
    }
}
