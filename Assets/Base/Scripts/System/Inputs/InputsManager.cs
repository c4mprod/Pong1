// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="InputsManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class InputsManager.
/// </summary>
public class InputsManager : IUpdateBehaviour
{
    #region "Events"

    /// <summary>
    /// Occurs when [move up event].
    /// </summary>
    public event CustomEventHandler MoveUpEvent;
    /// <summary>
    /// Occurs when [move down event].
    /// </summary>
    public event CustomEventHandler MoveDownEvent;
    /// <summary>
    /// Occurs when [shoot event].
    /// </summary>
    public event CustomEventHandler ShootEvent;
    /// <summary>
    /// Occurs when [pause event].
    /// </summary>
    public event CustomEventHandler PauseEvent;
    /// <summary>
    /// Occurs when [left event].
    /// </summary>
    public event CustomEventHandler LeftEvent;
    /// <summary>
    /// Occurs when [right event].
    /// </summary>
    public event CustomEventHandler RightEvent;
    /// <summary>
    /// Occurs when [return event].
    /// </summary>
    public event CustomEventHandler ReturnEvent;

    /// <summary>
    /// Occurs when [left mouse down event].
    /// </summary>
    public event CustomEventHandler LeftClickEvent;

    #endregion

    #region "EventArgs Value Objects"

    /// <summary>
    /// Class InputsVO.
    /// </summary>
    public class InputsVO : System.EventArgs
    {
        /// <summary>
        /// The m_ e player
        /// </summary>
        public GlobalDatasModel.EPlayer m_EPlayer;
    }

    /// <summary>
    /// Class ClickVO.
    /// </summary>
    public class ClickVO : System.EventArgs
    {
        /// <summary>
        /// The m_ axis value
        /// </summary>
        public float m_XAxisValue = 0.0f;
        public float m_YAxisValue = 0.0f;
    }

    #endregion

    /// <summary>
    /// The m_ player1 inputs
    /// </summary>
    private Dictionary<string, bool> m_Player1Inputs = new Dictionary<string, bool>();
    /// <summary>
    /// The m_ player2 inputs
    /// </summary>
    private Dictionary<string, bool> m_Player2Inputs = new Dictionary<string, bool>();
    /// <summary>
    /// The m_ general inputs
    /// </summary>
    private Dictionary<string, bool> m_GeneralInputs = new Dictionary<string, bool>();
    /// <summary>
    /// The m_ mouse inputs
    /// </summary>
    private Dictionary<string, bool> m_MouseInputs = new Dictionary<string, bool>();

    /// <summary>
    /// The m_ controls events
    /// </summary>
    private Dictionary<string, CustomEventHandler> m_ControlsEvents = new Dictionary<string, CustomEventHandler>();
    /// <summary>
    /// The m_ inputs vo
    /// </summary>
    private InputsVO m_InputsVO = new InputsVO();
    /// <summary>
    /// The m_ click vo
    /// </summary>
    private ClickVO m_ClickVO = new ClickVO();

    /// <summary>
    /// Initializes a new instance of the <see cref="InputsManager" /> class.
    /// </summary>
    public InputsManager()
    {
        MoveUpEvent += GameController.Instance.OnPlayerMoveUp;
        MoveDownEvent += GameController.Instance.OnPlayerMoveDown;
        ShootEvent += GameController.Instance.OnPlayerShoot;
        PauseEvent += GameController.Instance.OnPause;
        LeftEvent += GameController.Instance.OnLeft;
        RightEvent += GameController.Instance.OnRight;
        ReturnEvent += GameController.Instance.OnReturn;
        LeftClickEvent += GameController.Instance.OnLeftClick;

        this.m_ControlsEvents["MoveUp"] = MoveUpEvent;
        this.m_ControlsEvents["MoveDown"] = MoveDownEvent;
        this.m_ControlsEvents["Shoot"] = ShootEvent;
        this.m_ControlsEvents["Left"] = LeftEvent;
        this.m_ControlsEvents["Right"] = RightEvent;
        this.m_ControlsEvents["Return"] = ReturnEvent;
        this.m_ControlsEvents["Pause"] = PauseEvent;
        this.m_ControlsEvents["LeftClick"] = LeftClickEvent;

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
            this.m_Player1Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
            this.m_Player2Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
            this.m_GeneralInputs[lPair.Key] = false;
        foreach (KeyValuePair<string, int> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_MouseControls)
            this.m_MouseInputs[lPair.Key] = false;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            if (Input.GetKey(lPair.Value))
            {
                this.m_Player1Inputs[lPair.Key] = true;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            if (Input.GetKey(lPair.Value))
            {
                this.m_Player2Inputs[lPair.Key] = true;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
        {
            if (Input.GetKey(lPair.Value))
                this.m_GeneralInputs[lPair.Key] = true;
        }

        foreach (KeyValuePair<string, int> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_MouseControls)
        {
            if (Input.GetMouseButton(lPair.Value))
                this.m_MouseInputs[lPair.Key] = true;
        }
    }

    /// <summary>
    /// Fixeds the update.
    /// </summary>
    public void FixedUpdate()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            if (this.m_Player1Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.Player1;
                if (this.m_ControlsEvents[lPair.Key] != null)
                    this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player1Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            if (this.m_Player2Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.Player2;
                if (this.m_ControlsEvents[lPair.Key] != null)
                    this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player2Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
        {
            if (this.m_GeneralInputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.None;
                if (this.m_ControlsEvents[lPair.Key] != null)
                    this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_GeneralInputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, int> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_MouseControls)
        {
            if (this.m_MouseInputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_ClickVO.m_XAxisValue = Input.GetAxis("Mouse X");
                this.m_ClickVO.m_YAxisValue = Input.GetAxis("Mouse Y");
                if (this.m_ControlsEvents[lPair.Key] != null)
                    this.m_ControlsEvents[lPair.Key](null, this.m_ClickVO);
                this.m_MouseInputs[lPair.Key] = false;
            }
        }
    }

    /// <summary>
    /// Resets the inputs.
    /// </summary>
    public void ResetInputs()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            this.m_Player1Inputs[lPair.Key] = false;
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            this.m_Player2Inputs[lPair.Key] = false;
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
        {
            this.m_GeneralInputs[lPair.Key] = false;
        }

        foreach (KeyValuePair<string, int> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_MouseControls)
        {
            this.m_MouseInputs[lPair.Key] = false;
        }
    }
}
