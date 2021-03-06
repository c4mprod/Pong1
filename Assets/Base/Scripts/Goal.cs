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
    private GoalVO m_GoalVO = new GoalVO();

    void OnEnable()
    {
        this.m_GoalEvent += GameManager.Instance.OnGoal;
    }

    void OnDisable()
    {
        this.m_GoalEvent -= GameManager.Instance.OnGoal;
    }

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

            case "Shoot":
                if (collider.GetComponent<Shoot>().m_EPlayer != this.m_EPlayer)
                    collider.GetComponent<Shoot>().Disable();
                break;

            default:
                break;
        }
    }
}
