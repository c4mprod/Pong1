// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-05-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="BallSpawn.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// Class BallSpawn.
/// </summary>
public class BallSpawn : MonoBehaviour
{
    // Used to set the BallInstance velocity with the rights force and direction.
    /// <summary>
    /// The m_ spawn force
    /// </summary>
    private Vector3 m_SpawnForce;
    // The corresponding Ball Prefab.
    /// <summary>
    /// The m_ ball
    /// </summary>
    public GameObject m_Ball;
    // The spawn vector angle used by SpawnForce.
    /// <summary>
    /// The m_ spawn angle
    /// </summary>
    public Vector2 m_SpawnAngle;
    /// <summary>
    /// The m_ spawn force speed
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float m_SpawnForceSpeed = 1.0f;
    // The instance of the ball prefab.
    /// <summary>
    /// The m_ ball instance
    /// </summary>
    private GameObject m_BallInstance = null;

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        this.m_SpawnForce.Normalize();
        this.m_SpawnForce.x = Mathf.Sin(this.m_SpawnAngle.x * Mathf.Deg2Rad);
        this.m_SpawnForce.y = Mathf.Sin(this.m_SpawnAngle.y * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Spawns this instance.
    /// </summary>
    private void Spawn()
    {
        if (this.m_BallInstance == null)
        {
            this.m_BallInstance = (GameObject)Instantiate(this.m_Ball, this.transform.position, Quaternion.identity);
            this.m_BallInstance.transform.parent = this.transform.parent;
        }
        this.m_BallInstance.transform.position = this.transform.position;
        this.m_BallInstance.transform.rotation = Quaternion.identity;
        this.m_BallInstance.rigidbody2D.velocity = this.m_SpawnForce.normalized * this.m_SpawnForceSpeed;
    }

    #region "OnEnable / OnDisable"

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        GameController.GoalEvent += OnGoal;
        GameController.SpawnEvent += OnSpawn;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        GameController.GoalEvent -= OnGoal;
        GameController.SpawnEvent -= OnSpawn;
    }

    #endregion

    #region "Events functions"

    /// <summary>
    /// Handles the <see cref="E:Spawn" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnSpawn(Object _Obj, System.EventArgs _EventArg)
    {
        this.Spawn();
    }

    /// <summary>
    /// Handles the <see cref="E:GameController.GoalEvent" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {
        Goal.GoalVO lGoalVO = (Goal.GoalVO)_EventArg;

        if (GlobalDatasModel.Instance.IsScoreLimitReach())
        {
            if (this.m_BallInstance != null)
                Destroy(this.m_BallInstance.gameObject);
            this.m_BallInstance = null;
        }
        else
        {
            this.m_BallInstance.transform.position = this.transform.position;
            this.m_BallInstance.transform.rotation = Quaternion.identity;

            // Set the ball spawn direction to the goaling player.
            if ((this.m_SpawnForce.x > 0.0f && lGoalVO.m_EPlayer == GlobalDatasModel.EPlayer.Player2)
                || (this.m_SpawnForce.x < 0.0f && lGoalVO.m_EPlayer == GlobalDatasModel.EPlayer.Player1))
                this.m_SpawnForce.x *= -1;

            this.Spawn();
        }
    }

    #endregion
}
