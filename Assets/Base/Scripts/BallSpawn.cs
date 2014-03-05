using UnityEngine;
using System.Collections;

public class BallSpawn : MonoBehaviour 
{
    private Vector3 m_SpawnForce;
    public GameObject Ball;
    public Vector2 SpawnAngle;
    [Range(0.0f, 100.0f)]
    public float SpawnForceSpeed = 1.0f;
    private bool m_Spawn = true;
    private GameObject m_BallInstance;

    void Awake()
    {
        this.m_SpawnForce.Normalize();
        this.m_SpawnForce.x = Mathf.Sin(this.SpawnAngle.x * Mathf.Deg2Rad);
        this.m_SpawnForce.y = Mathf.Sin(this.SpawnAngle.y * Mathf.Deg2Rad);
    }

    void Start()
    {
        this.m_BallInstance = (GameObject)Instantiate(this.Ball, this.transform.position, Quaternion.identity);
        this.m_BallInstance.transform.parent = this.transform.parent;
    }

	void Update() 
    {
        if (this.m_Spawn)
        {
            this.m_BallInstance.transform.position = this.transform.position;
            this.m_BallInstance.transform.rotation = Quaternion.identity;
            this.m_BallInstance.rigidbody2D.velocity = this.m_SpawnForce.normalized * this.SpawnForceSpeed;
            this.m_Spawn = false;
        }
	}
}
