using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject m_PrefabShootsHolder;
    public GameController.EPlayer m_Player;
    public float m_MoveSpeed = 1.0f;

    private Vector2 m_Move = new Vector2();
    //private GameObject m_ShootsHolderInstance;
    private bool m_CanShoot = true;

    void Awake()
    {
        //TODO : Fix shoot.
        //this.m_ShootsHolderInstance = (GameObject)GameObject.Instantiate(this.m_PrefabShootsHolder);
        //this.m_ShootsHolderInstance.transform.position = this.transform.position;
        //this.m_ShootsHolderInstance.GetComponent<ShootsHolder>().Initialize(this.gameObject);
    }

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
        GameController.MoveUpEvent += this.OnMoveUp;
        GameController.MoveDownEvent += this.OnMoveDown;
        //GameManager.ShootEvent += this.OnShoot;
    }

    void OnDisable()
    {
        GameController.MoveUpEvent -= this.OnMoveUp;
        GameController.MoveDownEvent -= this.OnMoveDown;
        //GameManager.ShootEvent -= this.OnShoot;
    }

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
