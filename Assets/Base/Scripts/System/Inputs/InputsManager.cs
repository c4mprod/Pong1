using UnityEngine;
using System.Collections.Generic;

public class InputsManager : IUpdateBehaviour
{
    #region "Events"

    public event CustomEventHandler MoveUpEvent;
    public event CustomEventHandler MoveDownEvent;
    public event CustomEventHandler ShootEvent;
    public event CustomEventHandler PauseEvent;
    public event CustomEventHandler LeftEvent;
    public event CustomEventHandler RightEvent;
    public event CustomEventHandler ReturnEvent;

    #endregion

    #region "EventArgs Value Objects"

    public class InputsVO : System.EventArgs
    {
        public GlobalDatasModel.EPlayer m_EPlayer;
    }

    #endregion

    private Dictionary<string, bool> m_Player1Inputs = new Dictionary<string, bool>();
    private Dictionary<string, bool> m_Player2Inputs = new Dictionary<string, bool>();
    private Dictionary<string, bool> m_GeneralInputs = new Dictionary<string, bool>();
    private Dictionary<string, CustomEventHandler> m_ControlsEvents = new Dictionary<string, CustomEventHandler>();
    private InputsVO m_InputsVO = new InputsVO();

    public InputsManager()
    {
        MoveUpEvent += GameController.Instance.OnPlayerMoveUp;
        MoveDownEvent += GameController.Instance.OnPlayerMoveDown;
        ShootEvent += GameController.Instance.OnPlayerShoot;
        PauseEvent += GameController.Instance.OnPause;
        LeftEvent += GameController.Instance.OnLeft;
        RightEvent += GameController.Instance.OnRight;
        ReturnEvent += GameController.Instance.OnReturn;

        this.m_ControlsEvents["MoveUp"] = MoveUpEvent;
        this.m_ControlsEvents["MoveDown"] = MoveDownEvent;
        this.m_ControlsEvents["Shoot"] = ShootEvent;
        this.m_ControlsEvents["Left"] = LeftEvent;
        this.m_ControlsEvents["Right"] = RightEvent;
        this.m_ControlsEvents["Return"] = ReturnEvent;
        this.m_ControlsEvents["Pause"] = PauseEvent;

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
            this.m_Player1Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
            this.m_Player2Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
            this.m_GeneralInputs[lPair.Key] = false;
    }

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
    }

    public void FixedUpdate()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            if (this.m_Player1Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.Player1;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player1Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            if (this.m_Player2Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.Player2;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player2Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatasModel.Instance.m_InputsBinding.m_GeneralControls)
        {
            if (this.m_GeneralInputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GlobalDatasModel.EPlayer.None;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_GeneralInputs[lPair.Key] = false;
            }
        }
    }

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
    }
}
