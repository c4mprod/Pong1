// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="GameController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Delegate CustomEventHandler.
/// </summary>
/// <param name="_Obj">The _ object.</param>
/// <param name="_EArgs">The <see cref="System.EventArgs" /> instance containing the event data.</param>
public delegate void CustomEventHandler(Object _Obj, System.EventArgs _EArgs);

/// <summary>
/// Class GameController.
/// <para>It is the main controller (MVC pattern) of the game logic.
/// It updates the GlobalDatasModel and warns of a change the associated views </para>
/// <para>Datas used: : <see cref="GlobalDatasModel.Instance.m_RacketsData" />,
/// <see cref="GlobalDatasModel.Instance.m_LevelDatas"/>, <see cref="GlobalDatasModel.Instance.m_Player1"/>,
/// <see cref="GlobalDatasModel.Instance.m_Player2"/></para>
/// <para>Views associated : <see cref="T:RacketSelectionGUIView" />,
/// <see cref="T:GameGUIView"/></para>
/// <para>Models associated : <see cref="T:GlobalDatasModel" /></para>
/// </summary>
public class GameController : SingletonBehaviour<GameController>
{
    #region "Enumerations"

    /// <summary>
    /// Enum State
    /// </summary>
    public enum State
    {
        /// <summary>
        /// The none
        /// </summary>
        None,
        /// <summary>
        /// The round start
        /// </summary>
        RoundStart,
        /// <summary>
        /// The round run
        /// </summary>
        RoundRun,
        /// <summary>
        /// The round end
        /// </summary>
        RoundEnd,
        /// <summary>
        /// The players selection
        /// </summary>
        RacketSelection,
        /// <summary>
        /// The pause
        /// </summary>
        Pause
    }

    #endregion

    #region "Events"

    /// <summary>
    /// Occurs when [spawn event].
    /// </summary>
    public static event CustomEventHandler SpawnEvent;

    /// <summary>
    /// Occurs when [goal event].
    /// </summary>
    public static event CustomEventHandler GoalEvent;

    /// <summary>
    /// Occurs when [move up event].
    /// </summary>
    public static event CustomEventHandler MoveUpEvent;

    /// <summary>
    /// Occurs when [move down event].
    /// </summary>
    public static event CustomEventHandler MoveDownEvent;

    /// <summary>
    /// Occurs when [shoot event].
    /// </summary>
    public static event CustomEventHandler ShootEvent;

    /// <summary>
    /// Occurs when [round end event].
    /// </summary>
    public static event CustomEventHandler RoundEndEvent;

    /// <summary>
    /// Occurs when [right event].
    /// </summary>
    public static event CustomEventHandler RightEvent;

    /// <summary>
    /// Occurs when [left event].
    /// </summary>
    public static event CustomEventHandler LeftEvent;

    /// <summary>
    /// Occurs when [return event].
    /// </summary>
    public static event CustomEventHandler ReturnEvent;

    /// <summary>
    /// Occurs when [player selection changed event].
    /// </summary>
    public static event CustomEventHandler PlayerSelectionChangedEvent;

    public static event CustomEventHandler LeftClickEvent;

    #endregion

    #region "EventArgs Value Objects"

    /// <summary>
    /// Class WinnerVO.
    /// <para>Used by the GameController to send the winner to the <see cref="T:GameGUIView"/></para>>
    /// </summary>
    public class WinnerVO : System.EventArgs
    {
        /// <summary>
        /// The player identification.
        /// </summary>
        public GlobalDatasModel.EPlayer m_EPlayer;
    }

    /// <summary>
    /// Class SelectedRacketVO.
    /// <para>Used by the RacketSelectionGUIView to send the actual player racket selection to the GameController</para>
    /// </summary>
    public class SelectedRacketVO : System.EventArgs
    {
        /// <summary>
        /// The m_ player
        /// </summary>
        public GlobalDatasModel.EPlayer m_Player = GlobalDatasModel.EPlayer.None;
        /// <summary>
        /// The m_ racket selected position
        /// </summary>
        public int m_RacketSelectedPos = 0;
    }

    /// <summary>
    /// Class RacketSelectionPlayerVO.
    /// </summary>
    public class PlayerRacketSelectionVO : System.EventArgs
    {
        /// <summary>
        /// The m_ player
        /// </summary>
        public GlobalDatasModel.EPlayer m_Player = GlobalDatasModel.EPlayer.None;
    }

    #endregion

    #region "Member variables"

    /// <summary>
    /// The m_ game scene
    /// </summary>
    public string m_GameScene = "Game";
    /// <summary>
    /// The m_ racket selection scene
    /// </summary>
    public string m_RacketSelectionScene = "RacketSelection";
    public string m_Transition = "Transition";
    /// <summary>
    /// The m_ shoot delay
    /// </summary>
    public float m_ShootDelay = 1.0f;
    /// <summary>
    /// The m_ start timer delay
    /// </summary>
    public float m_StartTimerDelay = 5.0f;
    /// <summary>
    /// The m_ round end timer delay
    /// </summary>
    public float m_RoundEndTimerDelay = 5.0f;

    /// <summary>
    /// The inputs manager
    /// </summary>
    private InputsManager m_InputsManager = null;
    /// <summary>
    /// The winner value object
    /// </summary>
    private WinnerVO m_Winner = new WinnerVO();

    /// <summary>
    /// The player racket selection value object
    /// </summary>
    private PlayerRacketSelectionVO m_RacketSelectionPlayer = new PlayerRacketSelectionVO();

    /// <summary>
    /// The m_ time scale save
    /// </summary>
    private float m_TimeScaleSave = Time.timeScale;

    /// <summary>
    /// The m_ data current state
    /// </summary>
    private State m_DataCurrentState;
    /// <summary>
    /// Gets the state of the m_ current.
    /// </summary>
    /// <value>The state of the m_ current.</value>
    public State m_CurrentState
    {
        get
        {
            return this.m_DataCurrentState;
        }
    }

    /// <summary>
    /// The m_ data start timer
    /// </summary>
    private float m_DataStartTimer = 1.0f;
    /// <summary>
    /// Gets the m_ start timer.
    /// </summary>
    /// <value>The m_ start timer.</value>
    public float m_StartTimer
    {
        get
        {
            return this.m_DataStartTimer;
        }
    }

    /// <summary>
    /// The m_ data round end timer
    /// </summary>
    private float m_DataRoundEndTimer = 1.0f;
    /// <summary>
    /// Gets the m_ round end timer.
    /// </summary>
    /// <value>The m_ round end timer.</value>
    public float m_RoundEndTimer
    {
        get
        {
            return this.m_DataRoundEndTimer;
        }
    }


    #endregion

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize()
    {
        this.m_DataStartTimer = this.m_StartTimerDelay;
        this.m_InputsManager = new InputsManager();
        this.m_DataCurrentState = State.None;
        this.m_RacketSelectionPlayer.m_Player = GlobalDatasModel.EPlayer.None;

        if (GlobalDatasModel.Instance.m_RacketsData.m_RacketsList != null)
            this.ChangeState(State.RacketSelection);
        else
            this.ChangeState(State.RoundStart);
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update()
    {
        this.m_InputsManager.Update();
    }

    /// <summary>
    /// Fixed update.
    /// </summary>
    void FixedUpdate()
    {
        this.m_InputsManager.FixedUpdate();
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        GameController.GoalEvent = null;
        GameController.MoveUpEvent = null;
        GameController.MoveDownEvent = null;
        GameController.ShootEvent = null;
    }

    #region "States coroutines"

    /// <summary>
    /// The round start state.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator RoundStartState()
    {
        Application.LoadLevel(this.m_GameScene);

        GlobalDatasModel.Instance.m_LevelDatas.m_CurrentTime = 0.0f;
        this.m_DataStartTimer = this.m_StartTimerDelay;

        while (this.m_DataCurrentState == State.RoundStart
            && this.m_DataStartTimer > 0.0f)
        {
            this.m_DataStartTimer -= Time.deltaTime;
            yield return null;
        }

        GameController.SpawnEvent(this, null);
        this.ChangeState(State.RoundRun);
    }

    /// <summary>
    /// The round run state.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator RoundRunState()
    {
        while (this.m_DataCurrentState == State.RoundRun)
        {
            GlobalDatasModel.Instance.m_LevelDatas.m_CurrentTime += Time.deltaTime;

            /**
             ** When the score limit is reach, a Round End Event is trigged with the player who win.
             **/
            if (GlobalDatasModel.Instance.IsScoreLimitReach())
            {
                this.m_Winner.m_EPlayer =
                    GlobalDatasModel.Instance.m_Player1.m_Score > GlobalDatasModel.Instance.m_Player2.m_Score
                    ? GlobalDatasModel.EPlayer.Player1 : GlobalDatasModel.EPlayer.Player2;

                GameController.RoundEndEvent(this, this.m_Winner);
                this.ChangeState(State.RoundEnd);
            }

            yield return null;
        }
    }

    /// <summary>
    /// The round end state.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator RoundEndState()
    {
        this.m_DataRoundEndTimer = this.m_RoundEndTimerDelay;

        while (this.m_DataCurrentState == State.RoundEnd
            && this.m_DataRoundEndTimer > 0.0f)
        {
            this.m_DataRoundEndTimer -= Time.deltaTime;
            yield return null;
        }

        GlobalDatasModel.Instance.ResetScore();
        this.ChangeState(State.RoundStart);
    }

    /// <summary>
    /// The player selection racket state.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator RacketSelectionState()
    {
        Application.LoadLevel(this.m_RacketSelectionScene);
        while (this.m_DataCurrentState == State.RacketSelection)
        {
            yield return null;
        }
        Application.LoadLevel(this.m_Transition);
        Application.LoadLevel(this.m_GameScene);
    }

    /// <summary>
    /// The pause state.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator PauseState()
    {
        Time.timeScale = 0.0f;
        while (this.m_DataCurrentState == State.Pause)
        {
            yield return null;
        }
    }

    #endregion

    #region "States functions"

    /// <summary>
    /// Changes the state.
    /// </summary>
    /// <param name="_NewState">New state of the _.</param>
    private void ChangeState(State _NewState)
    {
        this.m_InputsManager.ResetInputs();

        if (_NewState != this.m_DataCurrentState)
        {
            this.m_DataCurrentState = _NewState;
            string lMethodName = _NewState.ToString() + "State";

            /**
             ** We invoke the proper non public coroutine's instance associate with the new state's name
             ** by C# reflection.
             **/
            System.Reflection.MethodInfo lMethodInfos =
                this.GetType().GetMethod(lMethodName, System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance);
            StartCoroutine((IEnumerator)lMethodInfos.Invoke(this, null));
        }
    }

    #endregion

    #region "Events functions"

    /// <summary>
    /// Handles the <see cref="E:Quit" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnQuit(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.Pause)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Continue" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnContinue(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.Pause)
        {
            Time.timeScale = this.m_TimeScaleSave;
            this.ChangeState(State.RoundRun);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Pause" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnPause(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            this.ChangeState(State.Pause);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Goal" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            Goal.GoalVO lVO = (Goal.GoalVO)_EventArg;

            /**
             ** If the player 2 goal is hit, the player 1 win points.
             **/
            GlobalDatasModel.Instance.CalculateScore(lVO.m_EPlayer, lVO.m_EGoalHitType);

            if (GameController.GoalEvent != null)
                GameController.GoalEvent(this, lVO);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:PlayerMoveUp" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnPlayerMoveUp(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            if (GameController.MoveUpEvent != null)
                GameController.MoveUpEvent(_Obj, _EventArg);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:PlayerMoveDown" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnPlayerMoveDown(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            if (GameController.MoveDownEvent != null)
                GameController.MoveDownEvent(_Obj, _EventArg);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:PlayerShoot" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnPlayerShoot(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            //GameManager.ShootEvent(_Obj, _EventArg);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Right" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnRight(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RacketSelection)
        {
            if (GameController.RightEvent != null)
                GameController.RightEvent(this, null);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Left" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnLeft(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RacketSelection)
        {
            if (GameController.LeftEvent != null)
                GameController.LeftEvent(this, null);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Return" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnReturn(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RacketSelection)
        {
            if (GameController.ReturnEvent != null)
                GameController.ReturnEvent(this, null);
        }
    }

    /// <summary>
    /// Handles the <see cref="E:RacketSelected" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    public void OnRacketSelected(Object _Obj, System.EventArgs _EventArg)
    {
        GameController.SelectedRacketVO lVO = (GameController.SelectedRacketVO)_EventArg;

        if (lVO.m_Player == GlobalDatasModel.EPlayer.Player1)
        {
            GlobalDatasModel.Instance.SetPlayerRacket(lVO.m_Player, lVO.m_RacketSelectedPos);

            this.m_RacketSelectionPlayer.m_Player = GlobalDatasModel.EPlayer.Player2;
            GameController.PlayerSelectionChangedEvent(this, this.m_RacketSelectionPlayer);
        }
        else if (lVO.m_Player == GlobalDatasModel.EPlayer.Player2)
        {
            GlobalDatasModel.Instance.SetPlayerRacket(lVO.m_Player, lVO.m_RacketSelectedPos);

            this.ChangeState(State.RoundStart);
        }
    }

    public void OnLeftClick(Object _Obj, System.EventArgs _EventArg)
    {
        if (GameController.LeftClickEvent != null)
            GameController.LeftClickEvent(_Obj, _EventArg);
    }

    #endregion
}
