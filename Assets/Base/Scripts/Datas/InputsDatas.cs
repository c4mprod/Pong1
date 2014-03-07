using UnityEngine;
using System.Collections.Generic;

public class InputsDatas : ScriptableObject
{
    public static readonly string InputsPath = "Assets/Base/Resources/Prefs/inputsBinding.asset";
    public Dictionary<string, KeyCode> m_Player1BindableControls = new Dictionary<string,KeyCode>();
    public Dictionary<string, KeyCode> m_Player2BindableControls = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> m_UnbindableControls = new Dictionary<string, KeyCode>();

    public static InputsDatas LoadPrefs()
    {
        InputsDatas lTmp = null;

        if ((lTmp = Resources.LoadAssetAtPath<InputsDatas>(InputsDatas.InputsPath)) == null)
            return (ScriptableObject.CreateInstance<InputsDatas>());
        return lTmp;
    }
}
