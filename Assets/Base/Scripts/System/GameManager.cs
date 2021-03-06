using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager>
{
    #region "Enumerations"

    public enum State
    {
        None,
        RoundStart,
        RoundRun,
        RoundEnd,
        Pause
    }

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

    #region "Events"

    public delegate void CustomEventHandler(Object _Obj, System.EventArgs _EArgs);
    public static event CustomEventHandler SpawnEvent;
    public static event CustomEventHandler GoalEvent;
    public static event CustomEventHandler MoveUpEvent;
    public static event CustomEventHandler MoveDownEvent;
    public static event CustomEventHandler ShootEvent;
    public static event CustomEventHandler RoundEndEvent;

    #endregion

    #region "EventArgs Value Objects"

    public class WinnerVO : System.EventArgs
    {
        public GameManager.EPlayer m_EPlayer;
    }

    #endregion

    #region "Member variables"

    public string m_StartScene = "Game";
    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;
    public float m_ShootDelay = 1.0f;
    public float m_StartTimerDelay = 5.0f;
    public float m_RoundEndTimerDelay = 5.0f;
    public int m_ScoreLimit = 5;

    private InputsManager m_InputsManager = null;
    private WinnerVO m_Winner = new WinnerVO();
    private float m_TimeScaleSave = Time.timeScale;

    private State m_DataCurrentState;
    public State m_CurrentState
    {
        get
        {
            return this.m_DataCurrentState;
        }
    }

    private float m_DataStartTimer = 1.0f;
    public float m_StartTimer
    {
        get
        {
            return this.m_DataStartTimer;
        }
    }

    private float m_DataRounEndTimer = 1.0f;
    public float m_RoundEndTimer
    {
        get
        {
            return this.m_DataRounEndTimer;
        }
    }


    #endregion

    public void Initialize()
    {
        this.m_DataStartTimer = this.m_StartTimerDelay;
        this.m_InputsManager = new InputsManager();
        this.m_DataCurrentState = State.None;
        this.ChangeState(State.RoundStart);
    }

    void Update()
    {
        this.m_InputsManager.Update();
    }

    void FixedUpdate()
    {
        this.m_InputsManager.FixedUpdate();
    }

    void OnDisable()
    {
        GameManager.GoalEvent = null;
        GameManager.MoveUpEvent = null;
        GameManager.MoveDownEvent = null;
        GameManager.ShootEvent = null;
    }

    #region "States coroutines"

    IEnumerator RoundStartState()
    {
        GlobalDatas.Instance.m_LevelDatas.m_CurrentTime = 0.0f;
        this.m_DataStartTimer = this.m_StartTimerDelay;

        while (this.m_DataCurrentState == State.RoundStart
            && this.m_DataStartTimer > 0.0f)
        {
            this.m_DataStartTimer -= Time.deltaTime;
            yield return null;
        }

        GameManager.SpawnEvent(this, null);
        this.ChangeState(State.RoundRun);
    }

    IEnumerator RoundRunState()
    {
        while (this.m_DataCurrentState == State.RoundRun)
        {
            GlobalDatas.Instance.m_LevelDatas.m_CurrentTime += Time.deltaTime;

            /**
             ** When the score limit is reach, a Round End Event is trigged with the player who win.
             **/
            if (this.IsScoreLimitReach())
            {
                this.m_Winner.m_EPlayer =
                    GlobalDatas.Instance.m_Player1.m_Score > GlobalDatas.Instance.m_Player2.m_Score
                    ? EPlayer.Player1 : EPlayer.Player2;

                GameManager.RoundEndEvent(this, this.m_Winner);
                this.ChangeState(State.RoundEnd);
            }

            yield return null;
        }
    }

    IEnumerator RoundEndState()
    {
        this.m_DataRounEndTimer = this.m_RoundEndTimerDelay;

        while (this.m_DataCurrentState == State.RoundEnd
            && this.m_DataRounEndTimer > 0.0f)
        {
            this.m_DataRounEndTimer -= Time.deltaTime;
            yield return null;
        }

        GlobalDatas.Instance.ResetScore();
        Application.LoadLevel(this.m_StartScene);
        this.ChangeState(State.RoundStart);
    }

    IEnumerator PauseState()
    {
        Time.timeScale = 0.0f;
        while (this.m_DataCurrentState == State.Pause)
        {
            yield return null;
        }
    }

    #endregion

    #region "States functions"

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

    #region "Utilities functions"

    public bool IsScoreLimitReach()
    {
        if (GlobalDatas.Instance.m_Player1.m_Score >= this.m_ScoreLimit
                || GlobalDatas.Instance.m_Player2.m_Score >= this.m_ScoreLimit)
            return true;
        return false;
    }

    private void CalculateScore(PlayerDatas _PlayerDatas, EGoalHitType _EGoalHitType)
    {
        switch (_EGoalHitType)
        {
            case EGoalHitType.Ball:
                _PlayerDatas.m_Score += this.m_BallScoreValue;
                break;

            case EGoalHitType.Enemy:
                _PlayerDatas.m_Score += this.m_EnemyScoreValue;
                break;

            default:
                break;
        }
    }

    #endregion

    #region "Events functions"

    public void OnQuit(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.Pause)
        {
            Application.Quit();
        }
    }

    public void OnContinue(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.Pause)
        {
            Time.timeScale = this.m_TimeScaleSave;
            this.ChangeState(State.RoundRun);
        }
    }

    public void OnPause(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            this.ChangeState(State.Pause);
        }
    }

    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            Goal.GoalVO lVO = (Goal.GoalVO)_EventArg;

            /**
             ** If the player 2 goal is hit, the player 1 win points.
             **/

            if (lVO.m_EPlayer == EPlayer.Player2)
                this.CalculateScore(GlobalDatas.Instance.m_Player1, lVO.m_EGoalHitType);
            else
                this.CalculateScore(GlobalDatas.Instance.m_Player2, lVO.m_EGoalHitType);

            GameManager.GoalEvent(this, lVO);
        }
    }

    public void OnPlayerMoveUp(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            GameManager.MoveUpEvent(_Obj, _EventArg);
        }
    }

    public void OnPlayerMoveDown(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            GameManager.MoveDownEvent(_Obj, _EventArg);
        }
    }

    public void OnPlayerShoot(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CurrentState == State.RoundRun)
        {
            //GameManager.ShootEvent(_Obj, _EventArg);
        }
    }

    #endregion
}
