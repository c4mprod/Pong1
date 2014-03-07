using UnityEngine;
using SerializerContract;
using SerializerFactory;
using SerializerException;
using System;

public class GlobalDatas : Singleton<GlobalDatas>
{
    #region "Datas"

    public PlayerDatas m_Player1;
    public PlayerDatas m_Player2;
    public LevelDatas m_LevelDatas;
    public InputsBindingDatas m_InputsBinding;

    #endregion

    #region "Data Initializations"

    private void InitializeInputsBinding()
    {
        this.m_InputsBinding = InputsBindingDatas.LoadPrefs();

        if (this.m_InputsBinding.m_Player1BindableControls.ContainsKey("MoveUp") == false)
        {
            this.m_InputsBinding.m_Player1BindableControls["MoveUp"] = KeyCode.Z;
            this.m_InputsBinding.m_Player1BindableControls["MoveDown"] = KeyCode.S;
            this.m_InputsBinding.m_Player1BindableControls["Shoot"] = KeyCode.Space;

            this.m_InputsBinding.m_Player2BindableControls["MoveUp"] = KeyCode.UpArrow;
            this.m_InputsBinding.m_Player2BindableControls["MoveDown"] = KeyCode.DownArrow;
            this.m_InputsBinding.m_Player2BindableControls["Shoot"] = KeyCode.Keypad0;
        }
    }

    #endregion

    public void Initialize()
    {
        this.m_Player1 = new PlayerDatas();
        this.m_Player2 = new PlayerDatas();
        this.m_LevelDatas = new LevelDatas();
        this.InitializeInputsBinding();
    }

    public void ResetScore()
    {
        this.m_Player1.m_Score = 0.0f;
        this.m_Player2.m_Score = 0.0f;
    }

    public GlobalDatas()
    {
        this.Initialize();
    }
}
