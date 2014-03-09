using UnityEngine;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    #region "Events"

    public event GameManager.CustomEventHandler m_DisableEvent;

    #endregion

    public float m_ShootSpeed = 5.0f;
    public GameManager.EPlayer m_EPlayer = GameManager.EPlayer.None;
    private Vector2 m_Move = new Vector2(0.0f, 0.0f);
    private Vector2 m_Position = new Vector2(0.0f, 0.0f);
    private Vector2 m_PositionSave = new Vector2(0.0f, 0.0f);

    void OnDisable()
    {
        this.m_DisableEvent = null;
    }

    void Update()
    {
        this.rigidbody2D.velocity = this.m_Move;
    }

    public void Initialize(Vector2 _Position, GameManager.EPlayer _EPlayer)
    {
        this.m_EPlayer = _EPlayer;
        this.transform.position = _Position;
        this.m_PositionSave = this.transform.position;
        this.m_Move.x = this.m_ShootSpeed;
    }

    public void Disable()
    {
        this.m_Position.x = this.m_PositionSave.x;
        this.m_Position.y = this.m_PositionSave.y;
        this.transform.position = this.m_Position;
        this.m_DisableEvent(this.gameObject, null);
    }

    public void SetMoveDirection(float direction)
    {
        this.m_Move.x = direction * this.m_ShootSpeed;
    }

    public void SetVerticalPosition(float y)
    {
        this.m_Position = this.transform.position;
        this.m_Position.y = y;
        this.transform.position = this.m_Position;
    }

}
