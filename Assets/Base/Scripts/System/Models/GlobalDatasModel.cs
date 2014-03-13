// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="GlobalDatasModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System;

/// <summary>
/// Class GlobalDatasModel.
/// </summary>
public class GlobalDatasModel : Singleton<GlobalDatasModel>
{
    #region "Enumerations"

    /// <summary>
    /// Enum EPlayer
    /// </summary>
    public enum EPlayer
    {
        /// <summary>
        /// The none
        /// </summary>
        None,
        /// <summary>
        /// The player1
        /// </summary>
        Player1,
        /// <summary>
        /// The player2
        /// </summary>
        Player2
    }

    /// <summary>
    /// Enum EGoalHitType
    /// </summary>
    public enum EGoalHitType
    {
        /// <summary>
        /// The ball
        /// </summary>
        Ball,
        /// <summary>
        /// The enemy
        /// </summary>
        Enemy
    }

    #endregion

    #region "Datas"

    /// <summary>
    /// The m_ player1
    /// </summary>
    public PlayerDatas m_Player1;
    /// <summary>
    /// The m_ player2
    /// </summary>
    public PlayerDatas m_Player2;
    /// <summary>
    /// The m_ level datas
    /// </summary>
    public LevelDatas m_LevelDatas;
    /// <summary>
    /// The m_ inputs binding
    /// </summary>
    public InputsDatas m_InputsBinding;
    /// <summary>
    /// The m_ rackets data
    /// </summary>
    public RacketsDatas m_RacketsData;
    /// <summary>
    /// The m_ ball score value
    /// </summary>
    public float m_BallScoreValue = 1.0f;
    /// <summary>
    /// The m_ enemy score value
    /// </summary>
    public float m_EnemyScoreValue = 0.2f;
    /// <summary>
    /// The m_ score limit
    /// </summary>
    public int m_ScoreLimit = 5;

    #endregion

    #region "Data Initializations"

    /// <summary>
    /// Initializes the inputs binding.
    /// </summary>
    private void InitializeInputsBinding()
    {
        this.m_InputsBinding = InputsDatas.Load(InputsDatas.EditorPath, InputsDatas.PlayPath);

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

            this.m_InputsBinding.m_MouseControls["LeftClick"] = 0;
        }
    }

    #endregion

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize()
    {
        this.m_Player1 = new PlayerDatas();
        this.m_Player2 = new PlayerDatas();
        this.m_LevelDatas = new LevelDatas();
        this.m_RacketsData = RacketsDatas.Load(RacketsDatas.EditorPath, RacketsDatas.PlayPath);

        this.InitializeInputsBinding();
    }

    /// <summary>
    /// Resets the score.
    /// </summary>
    public void ResetScore()
    {
        this.m_Player1.m_Score = 0.0f;
        this.m_Player2.m_Score = 0.0f;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalDatasModel" /> class.
    /// </summary>
    public GlobalDatasModel()
    {
        this.Initialize();
    }

    #region "Datas utilities"

    /// <summary>
    /// Calculates the score.
    /// </summary>
    /// <param name="_Player">The _ player.</param>
    /// <param name="_GoalHitType">Type of the _ goal hit.</param>
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

    /// <summary>
    /// Determines whether [is score limit reach].
    /// </summary>
    /// <returns><c>true</c> if [is score limit reach]; otherwise, <c>false</c>.</returns>
    public bool IsScoreLimitReach()
    {
        if (this.m_Player1.m_Score >= this.m_ScoreLimit
                || this.m_Player2.m_Score >= this.m_ScoreLimit)
            return true;
        return false;
    }

    /// <summary>
    /// Sets the player racket.
    /// </summary>
    /// <param name="_Player">The _ player.</param>
    /// <param name="_RacketDataPosition">The _ racket data position.</param>
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
