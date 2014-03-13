// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-11-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="RacketSelectionGUIView.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// <para>Class RacketSelectionGUIView.</para>
/// <para>It is the view linked to the Racket Selection at the beginning of the game.
/// Using
/// <see cref="M:GeneralHelpers.CalculateCollectionPositions" /> from
/// <see cref="T:GeneralHelpers" />
/// to display 3 Racket Datas.</para>
/// <para>Datas used: : <see cref="F:GlobalDatasModel.Instance.m_RacketsData.m_RacketsList" /></para>
/// <para>Controllers associated : <see cref="T:GameController" /></para>
/// <para>Models associated : <see cref="T:GlobalDatasModel" /></para>
/// </summary>
public class RacketSelectionGUIView : MonoBehaviour
{
    #region "Events"

    /// <summary>
    /// Occurs when [m_ on racket selected event].
    /// </summary>
    private event CustomEventHandler m_OnRacketSelectedEvent;

    #endregion

    #region "Rect positions"

    /// <summary>
    /// The global box
    /// </summary>
    private Rect m_GlobalBox;
    /// <summary>
    /// The box1
    /// </summary>
    private Rect m_Box1;
    /// <summary>
    /// The m_ box2
    /// </summary>
    private Rect m_Box2;
    /// <summary>
    /// The m_ box3
    /// </summary>
    private Rect m_Box3;
    /// <summary>
    /// The name label
    /// </summary>
    private Rect m_NameLabel;
    /// <summary>
    /// The m_ width label
    /// </summary>
    private Rect m_WidthLabel;
    /// <summary>
    /// The m_ speed label
    /// </summary>
    private Rect m_SpeedLabel;
    /// <summary>
    /// The select racket button
    /// </summary>
    private Rect m_SelectRacketButton;

    #endregion

    /// <summary>
    /// The current player
    /// </summary>
    private GlobalDatasModel.EPlayer m_CurrentPlayer = GlobalDatasModel.EPlayer.Player1;
    /// <summary>
    /// The current position
    /// </summary>
    private int m_CurrentPos = 1;
    /// <summary>
    /// The previous position
    /// </summary>
    private int m_PreviousPos = 0;
    /// <summary>
    /// The next position
    /// </summary>
    private int m_NextPos = 0;
    /// <summary>
    /// The action timer
    /// </summary>
    private float m_ActionTimer = 0f;
    /// <summary>
    /// The racket selection vo
    /// </summary>
    private GameController.SelectedRacketVO m_RacketSelectionVO = new GameController.SelectedRacketVO();

    /// <summary>
    /// The action timer delay
    /// </summary>
    public float m_ActionTimerDelay = 0.5f;

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        this.m_GlobalBox = new Rect(Screen.width / 2 - 600, Screen.height / 2 - 250, 1200, 500);
        this.m_Box1 = new Rect(Screen.width / 2 - 350, Screen.height / 2 - 100, 200, 200);
        this.m_Box2 = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        this.m_Box3 = new Rect(Screen.width / 2 + 150, Screen.height / 2 - 100, 200, 200);
        this.m_NameLabel = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 500, 100);
        this.m_WidthLabel = new Rect(Screen.width / 2 - 80, Screen.height / 2 + 130, 500, 100);
        this.m_SpeedLabel = new Rect(Screen.width / 2 - 80, Screen.height / 2 + 160, 500, 100);
        this.m_SelectRacketButton = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 300, 200, 100);
    }

    /// <summary>
    /// Called when [enable].
    /// </summary>
    void OnEnable()
    {
        this.m_OnRacketSelectedEvent += GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent += this.OnPlayerSelectionChanged;
        GameController.LeftEvent += this.OnLeft;
        GameController.RightEvent += this.OnRight;
        GameController.ReturnEvent += this.OnReturn;
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    void OnDisable()
    {
        this.m_OnRacketSelectedEvent -= GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent -= this.OnPlayerSelectionChanged;
        GameController.LeftEvent -= this.OnLeft;
        GameController.RightEvent -= this.OnRight;
        GameController.ReturnEvent -= this.OnReturn;
    }

    /// <summary>
    /// Called when [GUI].
    /// </summary>
    void OnGUI()
    {
        this.m_CurrentPos = GeneralHelpers.CalculateCollectionPositions(out this.m_PreviousPos, out this.m_NextPos,
             this.m_CurrentPos, GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count);

        GUI.Box(this.m_GlobalBox, "<size=30>" + m_CurrentPlayer + " Selection" + "</size>");
        GUI.Box(this.m_Box1,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_PreviousPos].m_Sprite.texture);
        GUI.Box(this.m_Box2,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Sprite.texture);
        GUI.Box(this.m_Box3,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_NextPos].m_Sprite.texture);

        GUI.Label(this.m_NameLabel, "<size=20>Racket Name : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Name + "</size>");
        GUI.Label(this.m_WidthLabel, "<size=20>Racket Width : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Width + "</size>");
        GUI.Label(this.m_SpeedLabel, "<size=20>Racket Speed : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Speed + "</size>");

        if (GUI.Button(this.m_SelectRacketButton, "<size=20> Select Racket </size>"))
        {
            this.OnReturn(this, null);
        }
    }

    /// <summary>
    /// The timer action.
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
    /// Handles the <see cref="E:Right" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnRight(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());
            this.m_CurrentPos += 1;
        }
    }

    /// <summary>
    /// Handles the <see cref="E:Left" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void OnLeft(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());
            this.m_CurrentPos -= 1;
        }
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

            this.m_RacketSelectionVO.m_RacketSelectedPos = this.m_CurrentPos;
            this.m_RacketSelectionVO.m_Player = this.m_CurrentPlayer;
            this.m_OnRacketSelectedEvent(this, this.m_RacketSelectionVO);
        }
    }

    #endregion
}
