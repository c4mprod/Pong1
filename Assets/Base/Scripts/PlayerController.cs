// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : BlackWires
// Created          : 03-06-2014
//
// Last Modified By : BlackWires
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="PlayerController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Class PlayerController.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The m_ prefab shoots holder
    /// </summary>
    public GameObject m_PrefabShootsHolder;
    /// <summary>
    /// The m_ player
    /// </summary>
    public GlobalDatasModel.EPlayer m_Player;
    /// <summary>
    /// The m_ move speed
    /// </summary>
    public float m_MoveSpeed = 1.0f;

    /// <summary>
    /// The m_ move
    /// </summary>
    private Vector2 m_Move = new Vector2();
    //private GameObject m_ShootsHolderInstance;
    /// <summary>
    /// The m_ can shoot
    /// </summary>
    private bool m_CanShoot = true;

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        //TODO : Fix shoot.
        //this.m_ShootsHolderInstance = (GameObject)GameObject.Instantiate(this.m_PrefabShootsHolder);
        //this.m_ShootsHolderInstance.transform.position = this.transform.position;
        //this.m_ShootsHolderInstance.GetComponent<ShootsHolder>().Initialize(this.gameObject);
        if (this.m_Player == GlobalDatasModel.EPlayer.Player1)
            GameController.Instance.m_Player1 = this.gameObject;
        else if (this.m_Player == GlobalDatasModel.EPlayer.Player2)
            GameController.Instance.m_Player2 = this.gameObject;
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start()
    {
        this.m_Move.y = 0.0f;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update()
    {
        this.m_Move.y = 0.0f;
        this.rigidbody2D.velocity = this.m_Move;
    }

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        GameController.MoveUpEvent += this.OnMoveUp;
        GameController.MoveDownEvent += this.OnMoveDown;
        //GameManager.ShootEvent += this.OnShoot;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        GameController.MoveUpEvent -= this.OnMoveUp;
        GameController.MoveDownEvent -= this.OnMoveDown;
        //GameManager.ShootEvent -= this.OnShoot;
    }

    /// <summary>
    /// Shoots the timer coroutine.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator ShootTimerCoroutine()
    {
        float lTimer = 0.0f;

        this.m_CanShoot = false;
        while (lTimer < GameController.Instance.m_ShootDelay)
        {
            yield return new WaitForEndOfFrame();
            lTimer += Time.deltaTime;
        }
        this.m_CanShoot = true;
    }

    #region "Events functions"

    /// <summary>
    /// Handles the <see cref="E:MoveUp" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    public void OnMoveUp(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

        if (lInputsVO.m_EPlayer == this.m_Player)
        {
            this.m_Move.y = this.m_MoveSpeed;
            this.rigidbody2D.velocity = this.m_Move;
        }
    }

    /// <summary>
    /// Handles the <see cref="E:MoveDown" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    public void OnMoveDown(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

        if (lInputsVO.m_EPlayer == this.m_Player)
        {
            this.m_Move.y = -this.m_MoveSpeed;
            this.rigidbody2D.velocity = this.m_Move;
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Shoot" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    public void OnShoot(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

       if (this.m_CanShoot)
        {
            if (lInputsVO.m_EPlayer == this.m_Player)
            {
                //this.m_ShootsHolderInstance.GetComponent<ShootsHolder>().Shoot(lInputsVO.m_EPlayer);
            }
            StartCoroutine(this.ShootTimerCoroutine());
        }
    }

    #endregion
}
