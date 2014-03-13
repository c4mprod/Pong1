// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-07-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="Shoot.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class Shoot.
/// </summary>
public class Shoot : MonoBehaviour
{
    #region "Events"

    /// <summary>
    /// Occurs when [m_ disable event].
    /// </summary>
    public event CustomEventHandler m_DisableEvent;

    #endregion

    /// <summary>
    /// The m_ shoot speed
    /// </summary>
    public float m_ShootSpeed = 5.0f;
    /// <summary>
    /// The m_ e player
    /// </summary>
    public GlobalDatasModel.EPlayer m_EPlayer = GlobalDatasModel.EPlayer.None;
    /// <summary>
    /// The m_ move
    /// </summary>
    private Vector2 m_Move = new Vector2(0.0f, 0.0f);
    /// <summary>
    /// The m_ position
    /// </summary>
    private Vector2 m_Position = new Vector2(0.0f, 0.0f);
    /// <summary>
    /// The m_ position save
    /// </summary>
    private Vector2 m_PositionSave = new Vector2(0.0f, 0.0f);

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        this.m_DisableEvent = null;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update()
    {
        this.rigidbody2D.velocity = this.m_Move;
    }

    /// <summary>
    /// Initializes the specified _ position.
    /// </summary>
    /// <param name="_Position">The _ position.</param>
    /// <param name="_EPlayer">The _ e player.</param>
    public void Initialize(Vector2 _Position, GlobalDatasModel.EPlayer _EPlayer)
    {
        this.m_EPlayer = _EPlayer;
        this.transform.position = _Position;
        this.m_PositionSave = this.transform.position;
        this.m_Move.x = this.m_ShootSpeed;
    }

    /// <summary>
    /// Disables this instance.
    /// </summary>
    public void Disable()
    {
        this.m_Position.x = this.m_PositionSave.x;
        this.m_Position.y = this.m_PositionSave.y;
        this.transform.position = this.m_Position;
        this.m_DisableEvent(this.gameObject, null);
    }

    /// <summary>
    /// Sets the move direction.
    /// </summary>
    /// <param name="direction">The direction.</param>
    public void SetMoveDirection(float direction)
    {
        this.m_Move.x = direction * this.m_ShootSpeed;
    }

    /// <summary>
    /// Sets the vertical position.
    /// </summary>
    /// <param name="y">The y.</param>
    public void SetVerticalPosition(float y)
    {
        this.m_Position = this.transform.position;
        this.m_Position.y = y;
        this.transform.position = this.m_Position;
    }

}
