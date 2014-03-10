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

        GameController.Instance.transform.parent = this.transform;
        GameController.Instance.m_StartScene = this.m_StartScene;
        GameController.Instance.m_BallScoreValue = this.m_BallScoreValue;
        GameController.Instance.m_EnemyScoreValue = this.m_EnemyScoreValue;
        GameController.Instance.m_ShootDelay = this.m_ShootDelay;
        GameController.Instance.m_StartTimerDelay = this.m_StartTimerDelay;
        GameController.Instance.m_ScoreLimit = this.m_ScoreLimit;
        GameController.Instance.m_RoundEndTimerDelay = this.m_RoundEndTimerDelay;
        GameController.Instance.Initialize();

        Application.LoadLevel(this.m_StartScene);

        Destroy(this);
	}
}
