using UnityEngine;
using System.Collections;

public class BallSpawn : MonoBehaviour 
{
    // Used to set the BallInstance velocity with the rights force and direction.
    private Vector3 m_SpawnForce;
    // The corresponding Ball Prefab.
    public GameObject m_Ball;
    // The spawn vector angle used by SpawnForce.
    public Vector2 m_SpawnAngle;
    [Range(0.0f, 100.0f)]
    public float m_SpawnForceSpeed = 1.0f;
    private bool m_Spawn = true;
    // The instance of the ball prefab.
    private GameObject m_BallInstance;

    void Awake()
    {
        this.m_SpawnForce.Normalize();
        this.m_SpawnForce.x = Mathf.Sin(this.m_SpawnAngle.x * Mathf.Deg2Rad);
        this.m_SpawnForce.y = Mathf.Sin(this.m_SpawnAngle.y * Mathf.Deg2Rad);
    }

    void Start()
    {
        this.m_BallInstance = (GameObject)Instantiate(this.m_Ball, this.transform.position, Quaternion.identity);
        this.m_BallInstance.transform.parent = this.transform.parent;
    }

    #region "OnEnable / OnDisable"

    void OnEnable()
    {
        GameManager.GoalEvent += OnSpawn;
    }

    void OnDisable()
    {
        GameManager.GoalEvent -= OnSpawn;
    }

    #endregion

    void OnSpawn(Object _Obj, System.EventArgs _EventArg)
    {
        Goal.GoalVO lGoalVO = (Goal.GoalVO)_EventArg;

        this.m_BallInstance.transform.position = this.transform.position;
        this.m_BallInstance.transform.rotation = Quaternion.identity;

        // Reverse the ball spawn direction against the goaling player.
        if ((this.m_SpawnForce.x > 0.0f && lGoalVO.m_EPlayer == GameManager.EPlayer.Player2)
            || (this.m_SpawnForce.x < 0.0f && lGoalVO.m_EPlayer == GameManager.EPlayer.Player1))
            this.m_SpawnForce.x *= -1;

        this.m_BallInstance.rigidbody2D.velocity = this.m_SpawnForce.normalized * this.m_SpawnForceSpeed;
        this.m_Spawn = false;
    }

	void Update() 
    {
        if (this.m_Spawn)
        {
            this.m_BallInstance.transform.position = this.transform.position;
            this.m_BallInstance.transform.rotation = Quaternion.identity;
            this.m_BallInstance.rigidbody2D.velocity = this.m_SpawnForce.normalized * this.m_SpawnForceSpeed;
            this.m_Spawn = false;
        }
	}
}
