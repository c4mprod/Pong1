using UnityEngine;
using System;

public class GlobalDatasModel : Singleton<GlobalDatasModel>
{
    #region "Enumerations"

    public enum EPlayer
    {
        None,
        Player1,
        Player2
    }

    public enum EGoalHitType
    {
        Ball,
        Enemy
    }

    #endregion

    #region "Datas"

    public PlayerDatas m_Player1;
    public PlayerDatas m_Player2;
    public LevelDatas m_LevelDatas;
    public InputsDatas m_InputsBinding;
    public RacketsDatas m_RacketsData;
    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;
    public int m_ScoreLimit = 5;

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

        this.InitializeInputsBinding();
    }

    public void ResetScore()
    {
        this.m_Player1.m_Score = 0.0f;
        this.m_Player2.m_Score = 0.0f;
    }

    public GlobalDatasModel()
    {
        this.Initialize();
    }

    #region "Datas utilities"

    public void CalculateScore(EPlayer _Player, EGoalHitType _GoalHitType)
    {
        float lScore = 0.0f;

        switch (_GoalHitType)
        {
            case EGoalHitType.Ball:
                lScore += this.m_BallScoreValue;
                break;

            case EGoalHitType.Enemy:
               lScore += this.m_EnemyScoreValue;
                break;

            default:
                break;
        }

        if (_Player == EPlayer.Player1)
            this.m_Player1.m_Score += lScore;
        else if (_Player == EPlayer.Player2)
            this.m_Player2.m_Score += lScore;
    }

    public bool IsScoreLimitReach()
    {
        if (this.m_Player1.m_Score >= this.m_ScoreLimit
                || this.m_Player2.m_Score >= this.m_ScoreLimit)
            return true;
        return false;
    }

    public void SetPlayerRacket(EPlayer _Player, int _RacketDataPosition)
    {
        if (_Player == EPlayer.Player1)
        {
            this.m_Player1.m_RacketDatas = this.m_RacketsData.m_RacketsList[_RacketDataPosition];
        }
        else if (_Player == EPlayer.Player2)
        {
            this.m_Player2.m_RacketDatas = this.m_RacketsData.m_RacketsList[_RacketDataPosition];
        }
    }

    #endregion
}
