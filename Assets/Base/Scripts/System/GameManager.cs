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
    public static event CustomEventHandler GoalEvent;
    public static event CustomEventHandler MoveUpEvent;
    public static event CustomEventHandler MoveDownEvent;
    public static event CustomEventHandler ShootEvent;

    #endregion

    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;
    public float m_ShootDelay = 1.0f;

    private InputsManager m_InputsManager = null;
    private bool m_CanShoot = true;
    private State m_CurrentState;

    void Awake()
    {
        this.m_InputsManager = new InputsManager();
        this.m_CurrentState = State.None;
        this.ChangeState(State.RoundRun);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (this.m_CurrentState == State.RoundRun)
            this.m_InputsManager.FixedUpdate();
    }

    void OnDisable()
    {
        GoalEvent = null;
        MoveUpEvent = null;
        MoveDownEvent = null;
        ShootEvent = null;
    }

    #region "States coroutines"

    IEnumerator RoundStartState()
    {
        while (this.m_CurrentState == State.RoundStart)
        {
            yield return null;
        }
    }

    IEnumerator RoundRunState()
    {
        while (this.m_CurrentState == State.RoundRun)
        {
            this.m_InputsManager.Update();
            yield return null;
        }
    }

    IEnumerator RoundEndState()
    {
        while (this.m_CurrentState == State.RoundEnd)
        {
            yield return null;
        }
    }

    IEnumerator PauseState()
    {
        while (this.m_CurrentState == State.Pause)
        {
            yield return null;
        }
    }

    #endregion

    #region "States functions"

    private void ChangeState(State _NewState)
    {
        if (_NewState != this.m_CurrentState)
        {
            this.m_CurrentState = _NewState;
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

    private IEnumerator ShootTimerCoroutine()
    {
        float lTimer = 0.0f;

        this.m_CanShoot = false;
        while (lTimer < this.m_ShootDelay)
        {
            yield return new WaitForEndOfFrame();
            lTimer += Time.deltaTime;
        }
        this.m_CanShoot = true;
    }

    #endregion

    #region "Events functions"

    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
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

    public void OnPlayerMoveUp(Object _Obj, System.EventArgs _EventArg)
    {
        GameManager.MoveUpEvent(_Obj, _EventArg);
    }

    public void OnPlayerMoveDown(Object _Obj, System.EventArgs _EventArg)
    {
        GameManager.MoveDownEvent(_Obj, _EventArg);
    }

    public void OnPlayerShoot(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_CanShoot)
        {
            StartCoroutine("ShootTimerCoroutine");
            GameManager.ShootEvent(_Obj, _EventArg);
        }
    }


    #endregion
}
