// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-06-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="Initializer.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class Initializer.
/// </summary>
public class Initializer : MonoBehaviour
{
    /// <summary>
    /// The game scene
    /// </summary>
    public string m_GameScene = "Game";

    /// <summary>
    /// The racket selection scene
    /// </summary>
    public string m_RacketSelectionScene = "RacketSelection";

    public string m_Transition = "Transition";
    /// <summary>
    /// The ball score value
    /// </summary>
    public float m_BallScoreValue = 1.0f;
    /// <summary>
    /// The enemy score value
    /// </summary>
    public float m_EnemyScoreValue = 0.2f;
    /// <summary>
    /// The shoot delay
    /// </summary>
    public float m_ShootDelay = 1.0f;
    /// <summary>
    /// The start timer delay
    /// </summary>
    public float m_StartTimerDelay = 5.0f;
    /// <summary>
    /// The round end timer delay
    /// </summary>
    public float m_RoundEndTimerDelay = 5.0f;
    /// <summary>
    /// The score limit
    /// </summary>
    public int m_ScoreLimit = 5;

    /// <summary>
    /// Awakes this instance.
    /// System GameObject is never destroyed, the first GameManager's instance is called
    /// and we attach GameManager to System.
    /// When GameManager is filled, the level is loaded and Initialize is destroyed.
    /// </summary>
	void Awake() 
	{
        DontDestroyOnLoad(this.gameObject);

        GlobalDatasModel.Instance.m_BallScoreValue = this.m_BallScoreValue;
        GlobalDatasModel.Instance.m_EnemyScoreValue = this.m_EnemyScoreValue;
        GlobalDatasModel.Instance.m_ScoreLimit = this.m_ScoreLimit;

        GameController.Instance.transform.parent = this.transform;
        GameController.Instance.m_GameScene = this.m_GameScene;
        GameController.Instance.m_RacketSelectionScene = this.m_RacketSelectionScene;
        GameController.Instance.m_ShootDelay = this.m_ShootDelay;
        GameController.Instance.m_StartTimerDelay = this.m_StartTimerDelay;
        GameController.Instance.m_RoundEndTimerDelay = this.m_RoundEndTimerDelay;
        GameController.Instance.Initialize();

        //Application.LoadLevel(this.m_GameScene);

        Destroy(this);
	}
}
