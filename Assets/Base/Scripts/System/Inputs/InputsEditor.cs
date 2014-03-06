using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class InputsEditor : EditorWindow
{
    public class ToogleHelper
    {
        public Dictionary<int, bool> m_ToogleIdDictionary = new Dictionary<int, bool>();
    }

    #region "Events"

    private event GameManager.CustomEventHandler m_ModifyInputEvent;

    #endregion

    private ToogleHelper m_ToogleHelper = new ToogleHelper();
    private Vector2 m_ScrollPosition;
    private int m_TooglePosition;

    [MenuItem("Custom/Inputs Editor")]
    public static void Init()
    {
        EditorWindow lWindow = EditorWindow.GetWindow<InputsEditor>("Inputs Editor", true);

        lWindow.minSize = new Vector2(500, 500);
    }

    void OnGUI()
    {
        this.m_TooglePosition = 0;
        this.m_ScrollPosition = EditorGUILayout.BeginScrollView(this.m_ScrollPosition, false, true);

        GlobalDatas.Instance.m_InputsBinding.m_Player1BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 1 controls");
        GlobalDatas.Instance.m_InputsBinding.m_Player2BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 2 controls");

        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Save", GUILayout.Width(200f)))
            {
                EditorUtility.SetDirty(GlobalDatas.Instance.m_InputsBinding);
                InputsBindingDatas lLoadTest = Resources.LoadAssetAtPath<InputsBindingDatas>(InputsBindingDatas.InputsPath);

                if (lLoadTest == null)
                    AssetDatabase.CreateAsset(GlobalDatas.Instance.m_InputsBinding, InputsBindingDatas.InputsPath);
                else
                    AssetDatabase.SaveAssets();
            }

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }
}
