using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager>
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

    void Awake()
    {
        this.m_InputsManager = new InputsManager();
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
        GoalEvent = null;
    }

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

        if (lVO.m_EPlayer == EPlayer.Player2)
            this.CalculateScore(GlobalDatas.Instance.m_Player1, lVO.m_EGoalHitType);
        else
            this.CalculateScore(GlobalDatas.Instance.m_Player2, lVO.m_EGoalHitType);

        GameManager.GoalEvent(this, lVO);
        //Debug.Log("Player1 Score : " + GlobalDatas.Instance.m_Player1.m_Score);
        //Debug.Log("Player2 Score : " + GlobalDatas.Instance.m_Player2.m_Score);
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
