using UnityEngine;
using System.Collections.Generic;


public class Goal : MonoBehaviour
{
    #region "Events"
    private event GameManager.CustomEventHandler m_GoalEvent;
    #endregion

    #region "EventArgs Value Objects"
    public class GoalVO : System.EventArgs
    {
        public GameManager.EPlayer m_EPlayer;
        public GameManager.EGoalHitType m_EGoalHitType;
    }
    #endregion

    public GameManager.EPlayer m_EPlayer;
    private GameManager m_GameManager;
    private GoalVO m_GoalVO = new GoalVO();

    #region "OnEnable / OnDisable"

    void OnEnable()
    {
        this.m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        this.m_GoalEvent += this.m_GameManager.OnGoal;
    }

    void OnDisable()
    {
        this.m_GoalEvent -= this.m_GameManager.OnGoal;
    }

    #endregion

    void Awake()
    {
        this.m_GoalVO.m_EPlayer = this.m_EPlayer;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Ball":
                this.m_GoalVO.m_EGoalHitType = GameManager.EGoalHitType.Ball;
                this.m_GoalEvent(this, this.m_GoalVO);
                break;

            case "Enemy":
                this.m_GoalVO.m_EGoalHitType = GameManager.EGoalHitType.Enemy;
                this.m_GoalEvent(this, this.m_GoalVO);
                break;

            default:
                break;
        }
    }
}
