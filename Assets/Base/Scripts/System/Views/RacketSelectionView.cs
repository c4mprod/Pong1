// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-12-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-13-2014
// ***********************************************************************
// <copyright file="RacketSelectionView.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Class RacketSelectionView.
/// </summary>
public class RacketSelectionView : MonoBehaviour 
{   
    #region "Events"

    /// <summary>
    /// Occurs when [m_ on racket selected event].
    /// </summary>
    private event CustomEventHandler m_OnRacketSelectedEvent;

    #endregion

    #region "Events Args VO"

    /// <summary>
    /// The m_ racket selection vo
    /// </summary>
    private GameController.SelectedRacketVO m_RacketSelectionVO = new GameController.SelectedRacketVO();

    #endregion
    /// <summary>
    /// The m_ current player
    /// </summary>
    private GlobalDatasModel.EPlayer m_CurrentPlayer = GlobalDatasModel.EPlayer.Player1;
    /// <summary>
    /// The m_ scroll order list
    /// </summary>
    private ScrollOrderList m_ScrollOrderList = null;
    /// <summary>
    /// The m_ action timer
    /// </summary>
    private float m_ActionTimer = 0f;

    /// <summary>
    /// The m_ player label
    /// </summary>
    public GameObject m_PlayerLabel = null;
    /// <summary>
    /// The m_ selection cam
    /// </summary>
    public Camera m_SelectionCam = null;
    /// <summary>
    /// The m_ order list
    /// </summary>
    public GameObject m_OrderList = null;
    /// <summary>
    /// The m_ scroll timer
    /// </summary>
    public float m_ScrollTimer = 0.1f;
    /// <summary>
    /// The m_ action timer delay
    /// </summary>
    public float m_ActionTimerDelay = 0.5f;

    /// <summary>
    /// Awakes this instance.
    /// </summary>
	void Awake() 
	{
        this.m_ScrollOrderList = this.m_OrderList.GetComponent<ScrollOrderList>();
	}

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        this.m_OnRacketSelectedEvent += GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent += this.OnPlayerSelectionChanged;
        GameController.LeftClickEvent += this.OnLeftClick;
        GameController.RightEvent += this.OnRight;
        GameController.LeftEvent += this.OnLeft;
        GameController.ReturnEvent += this.OnReturn;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        this.m_OnRacketSelectedEvent -= GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent -= this.OnPlayerSelectionChanged;
        GameController.LeftClickEvent -= this.OnLeftClick;
        GameController.LeftClickEvent -= this.OnLeftClick;
        GameController.LeftClickEvent -= this.OnLeftClick;
        GameController.ReturnEvent -= this.OnReturn;
    }

    /// <summary>
    /// Called when [GUI].
    /// </summary>
    void OnGUI()
    {
        this.m_PlayerLabel.GetComponent<UILabel>().text = m_CurrentPlayer + " Selection";
    }

    /// <summary>
    /// Actions the timer.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator ActionTimer()
    {
        this.m_ActionTimer = this.m_ActionTimerDelay;
        while (this.m_ActionTimer > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            this.m_ActionTimer -= Time.deltaTime;
        }
    }

    #region "Events functions"

    /// <summary>
    /// Handles the <see cref="E:PlayerSelectionChanged" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnPlayerSelectionChanged(Object _Obj, System.EventArgs _EventArg)
    {
        GameController.PlayerRacketSelectionVO lVO = (GameController.PlayerRacketSelectionVO)_EventArg;

        this.m_CurrentPlayer = lVO.m_Player;
    }

    /// <summary>
    /// Handles the <see cref="E:LeftClick" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnLeftClick(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.ClickVO lVO = (InputsManager.ClickVO)_EventArg;

        this.m_ScrollOrderList.MoveCam(-lVO.m_XAxisValue, lVO.m_YAxisValue);
    }

    /// <summary>
    /// Handles the <see cref="E:Right" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnRight(Object _Obj, System.EventArgs _EventArg)
    {
        this.m_ScrollOrderList.MoveCam(1.0f, 0.0f);
    }

    /// <summary>
    /// Handles the <see cref="E:Left" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnLeft(Object _Obj, System.EventArgs _EventArg)
    {
        this.m_ScrollOrderList.MoveCam(-1.0f, 0.0f);
    }

    /// <summary>
    /// Handles the <see cref="E:Return" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnReturn(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());

            this.m_RacketSelectionVO.m_RacketSelectedPos = this.m_ScrollOrderList.m_CurrentPosition;
            this.m_RacketSelectionVO.m_Player = this.m_CurrentPlayer;
            this.m_OnRacketSelectedEvent(this, this.m_RacketSelectionVO);
        }
    }

    /// <summary>
    /// Called when [click select racket].
    /// </summary>
    public void OnClickSelectRacket()
    {
        this.OnReturn(this, null);
    }

    #endregion
}
