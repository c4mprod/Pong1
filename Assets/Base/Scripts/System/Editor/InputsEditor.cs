using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class InputsEditor : EditorWindow
{
    public class ToogleHelper
    {
        public Dictionary<int, bool> m_ToogleIdDictionary = new Dictionary<int, bool>();
    }

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

        /**
         ** We are calling OnGUI extension class (see CustomEditorHelper) for each bindable controls dictionary.
         ** ToogleHelper remember selected control and we can associate keyboard event to simulate a bindable key button.
         **/

        GlobalDatas.Instance.m_InputsBinding.m_Player1BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 1 controls");
        GlobalDatas.Instance.m_InputsBinding.m_Player2BindableControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "Player 2 controls");
        GlobalDatas.Instance.m_InputsBinding.m_GeneralControls.OnGUI(this.m_ToogleHelper, ref this.m_TooglePosition, "General controls");

        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        {
            /**
             ** A save button which is going to save our custom bindable asset.
             ** It will be loaded at game start.
             **/
            if (GUILayout.Button("Save", GUILayout.Width(200f)))
            {
                GlobalDatas.Instance.m_InputsBinding.Save<InputsDatas>(InputsDatas.InputsPath);
            }

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }
}
