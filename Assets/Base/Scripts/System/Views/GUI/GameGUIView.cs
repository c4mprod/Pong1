// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-06-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="GameGUIView.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class GameGUIView.
/// </summary>
public class GameGUIView : MonoBehaviour
{
    #region "Events"

    /// <summary>
    /// Occurs when [continue event].
    /// </summary>
    public static event CustomEventHandler ContinueEvent;
    /// <summary>
    /// Occurs when [quit event].
    /// </summary>
    public static event CustomEventHandler QuitEvent;

    #endregion

    #region "GUI Rect"

    /// <summary>
    /// The m_ player1 rect
    /// </summary>
    private Rect m_Player1Rect;
    /// <summary>
    /// The m_ player2 rect
    /// </summary>
    private Rect m_Player2Rect;
    /// <summary>
    /// The m_ round run timer rect
    /// </summary>
    private Rect m_RoundRunTimerRect;
    /// <summary>
    /// The m_ continue button
    /// </summary>
    private Rect m_ContinueButton;
    /// <summary>
    /// The m_ quit buton
    /// </summary>
    private Rect m_QuitButon;

    #endregion

    /// <summary>
    /// The m_ round end MSG
    /// </summary>
    private string m_RoundEndMsg = "";
    /// <summary>
    /// Delegate GUI States Functions
    /// </summary>
    private delegate void StatesFunctions();
    /// <summary>
    /// The m_ states functions dictionary
    /// </summary>
    private Dictionary<GameController.State, StatesFunctions> m_StatesFunctionsDictionary = new Dictionary<GameController.State,StatesFunctions>();

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        this.m_Player1Rect = new Rect(0, 0, 200, 200);
        this.m_Player2Rect = new Rect(Screen.width - 250, 0, 500, 500);
        this.m_RoundRunTimerRect = new Rect(Screen.width / 2 - 100, 0, 500, 500);
        this.m_ContinueButton = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 20);
        this.m_QuitButon = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 20);

        this.m_StatesFunctionsDictionary[GameController.State.RoundStart] = this.RoundStart;
        this.m_StatesFunctionsDictionary[GameController.State.Pause] = this.Pause;
        this.m_StatesFunctionsDictionary[GameController.State.RoundRun] = this.RoundRun;
        this.m_StatesFunctionsDictionary[GameController.State.RoundEnd] = this.RoundEnd;
    }

    /// <summary>
    /// Rounds the end.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void RoundEnd(Object _Obj, System.EventArgs _EventArg)
    {
        GameController.WinnerVO lWinnerVO = (GameController.WinnerVO)_EventArg;

        // The player sent is the winner.
        this.m_RoundEndMsg = (lWinnerVO.m_EPlayer == GlobalDatasModel.EPlayer.Player1)
            ? "Player 1 Win" : "Player 2 Win";
    }

    #region "States Functions"

    /// <summary>
    /// Rounds the start.
    /// </summary>
    private void RoundStart()
    {
        GUI.Label(this.m_RoundRunTimerRect, "<size=40> Start in : " + (int)(GameController.Instance.m_StartTimer + 1.0f) + "</size>");
    }

    /// <summary>
    /// Rounds the run.
    /// </summary>
    private void RoundRun()
    {
        GUI.Label(this.m_Player1Rect, "<size=40> Score : " + GlobalDatasModel.Instance.m_Player1.m_Score + "</size>");
        GUI.Label(this.m_Player2Rect, "<size=40> Score : " + GlobalDatasModel.Instance.m_Player2.m_Score + "</size>");
        GUI.Label(this.m_RoundRunTimerRect, "<size=40> Time : " + GlobalDatasModel.Instance.m_LevelDatas.m_CurrentTime.FloatToTimeString() + "</size>");
    }

    /// <summary>
    /// Rounds the end.
    /// </summary>
    private void RoundEnd()
    {
        GUI.Label(this.m_RoundRunTimerRect, "<size=40>" + this.m_RoundEndMsg + "</size>");
    }

    /// <summary>
    /// Pauses this instance.
    /// </summary>
    private void Pause()
    {
        this.RoundRun();
        if (GUI.Button(this.m_ContinueButton, "Continue"))
            GameGUIView.ContinueEvent(this, null);
        if (GUI.Button(this.m_QuitButon, "Quit"))
            GameGUIView.QuitEvent(this, null);
    }

    #endregion

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        GameGUIView.ContinueEvent += GameController.Instance.OnContinue;
        GameGUIView.QuitEvent += GameController.Instance.OnQuit;
        GameController.RoundEndEvent += this.RoundEnd;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        GameGUIView.ContinueEvent -= GameController.Instance.OnContinue;
        GameGUIView.QuitEvent -= GameController.Instance.OnQuit;
        GameController.RoundEndEvent -= this.RoundEnd;
    }

    /// <summary>
    /// Called when [GUI].
    /// </summary>
    void OnGUI()
    {
        if (this.m_StatesFunctionsDictionary.ContainsKey(GameController.Instance.m_CurrentState))
            this.m_StatesFunctionsDictionary[GameController.Instance.m_CurrentState]();
    }
}
