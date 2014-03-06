using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameManager.EPlayer m_Player;
    public float m_MoveSpeed = 1.0f;
    private Vector2 m_Move = new Vector2();

    void Start()
    {
        this.m_Move.y = 0.0f;
    }

    void Update()
    {
        this.m_Move.y = 0.0f;
        this.rigidbody2D.velocity = this.m_Move;
    }

    void OnEnable()
    {
        GameManager.MoveUpEvent += this.OnMoveUp;
        GameManager.MoveDownEvent += this.OnMoveDown;
        GameManager.ShootEvent += this.OnShoot;
    }

    public void OnMoveUp(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

        if (lInputsVO.m_EPlayer == this.m_Player)
        {
            this.m_Move.y = this.m_MoveSpeed;
            this.rigidbody2D.velocity = this.m_Move;
        }
    }

    public void OnMoveDown(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

        if (lInputsVO.m_EPlayer == this.m_Player)
        {
            this.m_Move.y = -this.m_MoveSpeed;
            this.rigidbody2D.velocity = this.m_Move;
        }
    }

    public void OnShoot(Object _Obj, System.EventArgs _EventArg)
    {
        InputsManager.InputsVO lInputsVO = (InputsManager.InputsVO)_EventArg;

        if (lInputsVO.m_EPlayer == this.m_Player)
        {
        }
    }
}
