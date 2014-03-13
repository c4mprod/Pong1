// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="Goal.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Class Goal.
/// </summary>
public class Goal : MonoBehaviour
{
    #region "Events"

    /// <summary>
    /// Occurs when [m_ goal event].
    /// </summary>
    private event CustomEventHandler m_GoalEvent;

    #endregion

    #region "EventArgs Value Objects"

    /// <summary>
    /// Class GoalVO.
    /// </summary>
    public class GoalVO : System.EventArgs
    {
        /// <summary>
        /// The m_ e player
        /// </summary>
        public GlobalDatasModel.EPlayer m_EPlayer;
        /// <summary>
        /// The m_ e goal hit type
        /// </summary>
        public GlobalDatasModel.EGoalHitType m_EGoalHitType;
    }

    #endregion

    /// <summary>
    /// The m_ e player
    /// </summary>
    public GlobalDatasModel.EPlayer m_EPlayer;
    /// <summary>
    /// The m_ goal vo
    /// </summary>
    private GoalVO m_GoalVO = new GoalVO();

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        this.m_GoalEvent += GameController.Instance.OnGoal;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        this.m_GoalEvent -= GameController.Instance.OnGoal;
    }

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        this.m_GoalVO.m_EPlayer = this.m_EPlayer;
    }

    /// <summary>
    /// Called when [trigger enter2 d].
    /// </summary>
    /// <param name="collider">The collider.</param>
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
