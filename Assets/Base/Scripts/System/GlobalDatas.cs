using UnityEngine;
using SerializerContract;
using SerializerFactory;
using SerializerException;
using System;

public class GlobalDatas : Singleton<GlobalDatas>
{
    #region "Datas"

    public PlayerDatas m_Player1 = new PlayerDatas();
    public PlayerDatas m_Player2 = new PlayerDatas();
    public InputsBindingDatas m_InputsBinding;

    #endregion

    #region "Data loads"
    //public void LoadData<T>(ref T _Data, string _Path)
    //    where T : new()
    //{
    //    try
    //    {
    //        Debug.LogError("UNSERIALIZE : " + Application.dataPath + _Path);
    //        this.m_Sr.Unserialize<T>(ref _Data, Application.dataPath + _Path);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Cannot load : " + e.Message);
    //        if (_Data == null)
    //        {
    //            _Data = new T();
    //        }
    //    }
    //}
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
        this.InitializeInputsBinding();
    }

    public GlobalDatas()
    {
        this.Initialize();
    }
}
