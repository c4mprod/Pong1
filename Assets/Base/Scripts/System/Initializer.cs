using UnityEngine;
using System.Collections.Generic;

public class Initializer : MonoBehaviour
{
    public string m_StartScene = "Game";
    public float m_BallScoreValue = 1.0f;
    public float m_EnemyScoreValue = 0.2f;
    public float m_ShootDelay = 1.0f;
    public float m_StartTimerDelay = 5.0f;
    public float m_RoundEndTimerDelay = 5.0f;
    public int m_ScoreLimit = 5;

    /**
     ** System GameObject is never destroyed, the first GameManager's instance is called
     ** and we attach GameManager to System.
     ** When GameManager is filled, the level is loaded and Initialize is destroyed.
     **/
	void Awake() 
	{
        DontDestroyOnLoad(this.gameObject);

        GameManager.Instance.transform.parent = this.transform;
        GameManager.Instance.m_StartScene = this.m_StartScene;
        GameManager.Instance.m_BallScoreValue = this.m_BallScoreValue;
        GameManager.Instance.m_EnemyScoreValue = this.m_EnemyScoreValue;
        GameManager.Instance.m_ShootDelay = this.m_ShootDelay;
        GameManager.Instance.m_StartTimerDelay = this.m_StartTimerDelay;
        GameManager.Instance.m_ScoreLimit = this.m_ScoreLimit;
        GameManager.Instance.m_RoundEndTimerDelay = this.m_RoundEndTimerDelay;
        GameManager.Instance.Initialize();

        Application.LoadLevel(this.m_StartScene);

        Destroy(this);
	}
}
