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
    // The instance of the ball prefab.
    private GameObject m_BallInstance = null;

    void Awake()
    {
        this.m_SpawnForce.Normalize();
        this.m_SpawnForce.x = Mathf.Sin(this.m_SpawnAngle.x * Mathf.Deg2Rad);
        this.m_SpawnForce.y = Mathf.Sin(this.m_SpawnAngle.y * Mathf.Deg2Rad);
    }

    void Start()
    {
    }

    private void Spawn()
    {
        if (this.m_BallInstance == null)
        {
            this.m_BallInstance = (GameObject)Instantiate(this.m_Ball, this.transform.position, Quaternion.identity);
            this.m_BallInstance.transform.parent = this.transform.parent;
        }
        this.m_BallInstance.transform.position = this.transform.position;
        this.m_BallInstance.transform.rotation = Quaternion.identity;
        this.m_BallInstance.rigidbody2D.velocity = this.m_SpawnForce.normalized * this.m_SpawnForceSpeed;
    }

    #region "OnEnable / OnDisable"

    void OnEnable()
    {
        GameManager.GoalEvent += OnGoal;
        GameManager.SpawnEvent += OnSpawn;
    }

    void OnDisable()
    {
        GameManager.GoalEvent -= OnGoal;
        GameManager.SpawnEvent -= OnSpawn;
    }

    #endregion

    #region "Events functions"

    void OnSpawn(Object _Obj, System.EventArgs _EventArg)
    {
        this.Spawn();
    }

    void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {
        Goal.GoalVO lGoalVO = (Goal.GoalVO)_EventArg;

        if (GameManager.Instance.IsScoreLimitReach())
        {
            if (this.m_BallInstance != null)
                Destroy(this.m_BallInstance.gameObject);
            this.m_BallInstance = null;
        }
        else
        {
            this.m_BallInstance.transform.position = this.transform.position;
            this.m_BallInstance.transform.rotation = Quaternion.identity;

            // Set the ball spawn direction to the goaling player.
            if ((this.m_SpawnForce.x > 0.0f && lGoalVO.m_EPlayer == GameManager.EPlayer.Player2)
                || (this.m_SpawnForce.x < 0.0f && lGoalVO.m_EPlayer == GameManager.EPlayer.Player1))
                this.m_SpawnForce.x *= -1;

            this.Spawn();
        }
    }

    #endregion
}
