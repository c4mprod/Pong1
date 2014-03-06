using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonBehaviour<GameManager>
{
    #region "Enumerations"

    public enum EPlayer
    {
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

    #endregion

    private InputsManager m_InputsManager = new InputsManager();
    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;

    void Start()
    {
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
        Debug.Log("GameManager Disable");
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

    #endregion

    #region "Events functions"

    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {
        Goal.GoalVO lVO = (Goal.GoalVO)_EventArg;

        if (lVO.m_EPlayer == EPlayer.Player1)
            this.CalculateScore(GlobalDatas.Instance.m_Player1, lVO.m_EGoalHitType);
        else
            this.CalculateScore(GlobalDatas.Instance.m_Player2, lVO.m_EGoalHitType);

        GameManager.GoalEvent(this, lVO);
        Debug.Log("Player1 Score : " + GlobalDatas.Instance.m_Player1.m_Score);
        Debug.Log("Player2 Score : " + GlobalDatas.Instance.m_Player2.m_Score);
    }

    public void OnModifyInput(Object _Obj, System.EventArgs _EventArg)
    {
    }

    #endregion
}
