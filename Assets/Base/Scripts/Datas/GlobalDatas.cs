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
    public InputsDatas m_InputsBinding;
    public RacketsDatas m_RacketsData;

    #endregion

    #region "Data Initializations"

    private void InitializeInputsBinding()
    {
        this.m_InputsBinding = InputsDatas.Load(InputsDatas.InputsPath);

        if (this.m_InputsBinding.m_Player1BindableControls.ContainsKey("MoveUp") == false)
        {
            this.m_InputsBinding.m_Player1BindableControls["MoveUp"] = KeyCode.Z;
            this.m_InputsBinding.m_Player1BindableControls["MoveDown"] = KeyCode.S;
            this.m_InputsBinding.m_Player1BindableControls["Shoot"] = KeyCode.Space;

            this.m_InputsBinding.m_Player2BindableControls["MoveUp"] = KeyCode.UpArrow;
            this.m_InputsBinding.m_Player2BindableControls["MoveDown"] = KeyCode.DownArrow;
            this.m_InputsBinding.m_Player2BindableControls["Shoot"] = KeyCode.Keypad0;

            this.m_InputsBinding.m_GeneralControls["Pause"] = KeyCode.Escape;
            this.m_InputsBinding.m_GeneralControls["Left"] = KeyCode.LeftArrow;
            this.m_InputsBinding.m_GeneralControls["Right"] = KeyCode.RightArrow;
            this.m_InputsBinding.m_GeneralControls["Return"] = KeyCode.Return;
        }
    }

    #endregion

    public void Initialize()
    {
        this.m_Player1 = new PlayerDatas();
        this.m_Player2 = new PlayerDatas();
        this.m_LevelDatas = new LevelDatas();
        this.m_RacketsData = RacketsDatas.Load(RacketsDatas.Path);

        //// DEBUG
        //#region "DEBUG"

        //int i = -1;

        //while (++i < this.m_RacketsData.m_RacketsList.Count)
        //{
        //    Debug.Log("Racket[" + i + "] : " + this.m_RacketsData.m_RacketsList[i]);
        //}

        //#endregion


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
