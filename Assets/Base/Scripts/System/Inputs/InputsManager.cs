using UnityEngine;
using System.Collections.Generic;

public class InputsManager : IUpdateBehaviour
{
    #region "Events"

    public static event GameManager.CustomEventHandler MoveUpEvent;
    public static event GameManager.CustomEventHandler MoveDownEvent;
    public static event GameManager.CustomEventHandler ShootEvent;

    #endregion

    #region "EventArgs Value Objects"

    public class InputsVO : System.EventArgs
    {
        public GameManager.EPlayer m_EPlayer;
    }

    #endregion

    private Dictionary<string, bool> m_Player1Inputs = new Dictionary<string, bool>();
    private Dictionary<string, bool> m_Player2Inputs = new Dictionary<string, bool>();
    private Dictionary<string, bool> m_UnbindableInputs = new Dictionary<string, bool>();
    private Dictionary<string, GameManager.CustomEventHandler> m_ControlsEvents = new Dictionary<string, GameManager.CustomEventHandler>();
    private InputsVO m_InputsVO = new InputsVO();

    public InputsManager()
    {
        MoveUpEvent += GameManager.Instance.OnPlayerMoveUp;
        MoveDownEvent += GameManager.Instance.OnPlayerMoveDown;
        ShootEvent += GameManager.Instance.OnPlayerShoot;

        this.m_ControlsEvents["MoveUp"] = MoveUpEvent;
        this.m_ControlsEvents["MoveDown"] = MoveDownEvent;
        this.m_ControlsEvents["Shoot"] = ShootEvent;

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player1BindableControls)
            this.m_Player1Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player2BindableControls)
            this.m_Player2Inputs[lPair.Key] = false;
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_UnbindableControls)
            this.m_UnbindableInputs[lPair.Key] = false;
    }

    public void Update()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            if (Input.GetKey(lPair.Value))
                this.m_Player1Inputs[lPair.Key] = true;
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            if (Input.GetKey(lPair.Value))
                this.m_Player2Inputs[lPair.Key] = true;
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_UnbindableControls)
        {
            if (Input.GetKey(lPair.Value))
                this.m_UnbindableInputs[lPair.Key] = true;
        }
    }

    public void FixedUpdate()
    {
        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player1BindableControls)
        {
            if (this.m_Player1Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GameManager.EPlayer.Player1;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player1Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_Player2BindableControls)
        {
            if (this.m_Player2Inputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GameManager.EPlayer.Player2;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_Player2Inputs[lPair.Key] = false;
            }
        }

        foreach (KeyValuePair<string, KeyCode> lPair in GlobalDatas.Instance.m_InputsBinding.m_UnbindableControls)
        {
            if (this.m_UnbindableInputs[lPair.Key] == true && this.m_ControlsEvents.ContainsKey(lPair.Key))
            {
                this.m_InputsVO.m_EPlayer = GameManager.EPlayer.None;
                this.m_ControlsEvents[lPair.Key](null, this.m_InputsVO);
                this.m_UnbindableInputs[lPair.Key] = false;
            }
        }
    }
}
