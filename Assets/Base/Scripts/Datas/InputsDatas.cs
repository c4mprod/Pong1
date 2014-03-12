using UnityEngine;
using System.Collections.Generic;

public class InputsDatas : GenericCustomAsset<InputsDatas>
{
    public static readonly string PlayPath = "Prefs/inputsBinding";
    public static readonly string EditorPath = "Assets/Base/Resources/Prefs/inputsBinding.asset";

    public Dictionary<string, KeyCode> m_Player1BindableControls = new Dictionary<string,KeyCode>();
    public Dictionary<string, KeyCode> m_Player2BindableControls = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> m_GeneralControls = new Dictionary<string, KeyCode>();
}
