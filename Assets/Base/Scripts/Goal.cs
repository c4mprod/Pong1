using UnityEngine;
using System.Collections.Generic;


public class Goal : MonoBehaviour
{
    #region "Events"

    private event CustomEventHandler m_GoalEvent;

    #endregion

    #region "EventArgs Value Objects"

    public class GoalVO : System.EventArgs
    {
        public GlobalDatasModel.EPlayer m_EPlayer;
        public GlobalDatasModel.EGoalHitType m_EGoalHitType;
    }

    #endregion

    public GlobalDatasModel.EPlayer m_EPlayer;
    private GoalVO m_GoalVO = new GoalVO();

    void OnEnable()
    {
        this.m_GoalEvent += GameController.Instance.OnGoal;
    }

    void OnDisable()
    {
        this.m_GoalEvent -= GameController.Instance.OnGoal;
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
                this.m_GoalVO.m_EGoalHitType = GlobalDatasModel.EGoalHitType.Ball;
                this.m_GoalEvent(this, this.m_GoalVO);
                break;

            case "Enemy":
                this.m_GoalVO.m_EGoalHitType = GlobalDatasModel.EGoalHitType.Enemy;
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
