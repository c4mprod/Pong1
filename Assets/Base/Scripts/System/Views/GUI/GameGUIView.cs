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

    /// <summary>
    /// The m_ timer GUI
    /// </summary>
    public GameObject m_TimerGUI;
    /// <summary>
    /// The m_ score1
    /// </summary>
    public GameObject m_Score1;
    /// <summary>
    /// The m_ score2
    /// </summary>
    public GameObject m_Score2;
    /// <summary>
    /// The m_ continue
    /// </summary>
    public GameObject m_Continue;
    /// <summary>
    /// The m_ quit
    /// </summary>
    public GameObject m_Quit;

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
        //NGUITools.SetActive(this.m_TimerGUI, false);
        NGUITools.SetActive(this.m_Score1, false);
        NGUITools.SetActive(this.m_Score2, false);
        NGUITools.SetActive(this.m_Continue, false);
        NGUITools.SetActive(this.m_Quit, false);
        this.m_TimerGUI.GetComponent<UILabel>().text = "Start in : " + (int)(GameController.Instance.m_StartTimer + 1.0f);
    }

    /// <summary>
    /// Rounds the run.
    /// </summary>
    private void RoundRun()
    {
        //NGUITools.SetActive(this.m_TimerGUI, true);
        NGUITools.SetActive(this.m_Score1, true);
        NGUITools.SetActive(this.m_Score2, true);
        NGUITools.SetActive(this.m_Continue, false);
        NGUITools.SetActive(this.m_Quit, false);
        this.m_TimerGUI.GetComponent<UILabel>().text = "Time : " + GlobalDatasModel.Instance.m_LevelDatas.m_CurrentTime.FloatToTimeString();
        this.m_Score1.GetComponent<UILabel>().text = "Score : " + GlobalDatasModel.Instance.m_Player1.m_Score;
        this.m_Score2.GetComponent<UILabel>().text = "Score : " + GlobalDatasModel.Instance.m_Player2.m_Score;
     }

    /// <summary>
    /// Rounds the end.
    /// </summary>
    private void RoundEnd()
    {
        NGUITools.SetActive(this.m_Continue, false);
        NGUITools.SetActive(this.m_Quit, false);
        this.m_TimerGUI.GetComponent<UILabel>().text = this.m_RoundEndMsg;
    }

    /// <summary>
    /// Pauses this instance.
    /// </summary>
    private void Pause()
    {
        this.RoundRun();
        NGUITools.SetActive(this.m_Continue, true);
        NGUITools.SetActive(this.m_Quit, true);
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

    #region "Events functions"

    /// <summary>
    /// Called when [continue].
    /// </summary>
    public void OnContinue()
    {
        GameGUIView.ContinueEvent(this, null);
    }

    /// <summary>
    /// Called when [quit].
    /// </summary>
    public void OnQuit()
    {
        GameGUIView.QuitEvent(this, null);
    }

    #endregion
}
