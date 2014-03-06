using UnityEngine;
using System.Collections.Generic;

public class Initializer : MonoBehaviour
{
    public string m_StartScene = "Game";
    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;

	void Awake() 
	{
        DontDestroyOnLoad(this.gameObject);
        GameManager.Instance.transform.parent = this.transform;
        GameManager.Instance.m_BallScoreValue = this.m_BallScoreValue;
        GameManager.Instance.m_EnemyScoreValue = this.m_EnemyScoreValue;
        Application.LoadLevel(this.m_StartScene);
	}
}
