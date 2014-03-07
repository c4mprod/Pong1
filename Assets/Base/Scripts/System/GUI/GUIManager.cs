using UnityEngine;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    #region "Events"

    public static event GameManager.CustomEventHandler ContinueEvent;
    public static event GameManager.CustomEventHandler QuitEvent;

    #endregion

    #region "GUI Rect"

    private Rect m_Player1Rect;
    private Rect m_Player2Rect;
    private Rect m_RoundRunTimerRect;
    private Rect m_ContinueButton;
    private Rect m_QuitButon;

    #endregion

    private string m_RoundEndMsg = "";

    void Awake()
    {
        this.m_Player1Rect = new Rect(0, 0, 200, 200);
        this.m_Player2Rect = new Rect(Screen.width - 250, 0, 500, 500);
        this.m_RoundRunTimerRect = new Rect(Screen.width / 2 - 100, 0, 500, 500);
        this.m_ContinueButton = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 20);
        this.m_QuitButon = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 20);
    }

    void OnEnable()
    {
        GUIManager.ContinueEvent += GameManager.Instance.OnContinue;
        GUIManager.QuitEvent += GameManager.Instance.OnQuit;
        GameManager.RoundEndEvent += this.RoundEnd;
    }

    void OnDisable()
    {
        GUIManager.ContinueEvent -= GameManager.Instance.OnContinue;
        GUIManager.QuitEvent -= GameManager.Instance.OnQuit;
        GameManager.RoundEndEvent -= this.RoundEnd;
    }

    private void RoundEnd(Object _Obj, System.EventArgs _EventArg)
    {
        GameManager.WinnerVO lWinnerVO = (GameManager.WinnerVO)_EventArg;

        // The player sent is the winner.
        this.m_RoundEndMsg = (lWinnerVO.m_EPlayer == GameManager.EPlayer.Player1)
            ? "Player 1 Win" : "Player 2 Win";
    }

    void OnGUI()
    {
        if (GameManager.Instance.m_CurrentState == GameManager.State.RoundStart)
        {
            GUI.Label(this.m_RoundRunTimerRect, "<size=40> Start in : " + (int)GameManager.Instance.m_StartTimer + "</size>");
        }
        if (GameManager.Instance.m_CurrentState == GameManager.State.RoundRun
            || GameManager.Instance.m_CurrentState == GameManager.State.Pause)
        {
            int lMinutes = Mathf.FloorToInt(GlobalDatas.Instance.m_LevelDatas.m_CurrentTime / 60.0f);
            int lSeconds = Mathf.FloorToInt(GlobalDatas.Instance.m_LevelDatas.m_CurrentTime - lMinutes * 60);

            string lFormatedTime = string.Format("{0:0}:{1:00}", lMinutes, lSeconds);

            GUI.Label(this.m_Player1Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player1.m_Score + "</size>");
            GUI.Label(this.m_Player2Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player2.m_Score + "</size>");
            GUI.Label(this.m_RoundRunTimerRect, "<size=40> Time : " + lFormatedTime + "</size>");

        }
        if (GameManager.Instance.m_CurrentState == GameManager.State.RoundEnd)
        {
            GUI.Label(this.m_RoundRunTimerRect, "<size=40>" + this.m_RoundEndMsg + "</size>");
        }
        if (GameManager.Instance.m_CurrentState == GameManager.State.Pause)
        {
            if (GUI.Button(this.m_ContinueButton, "Continue"))
                GUIManager.ContinueEvent(this, null);
            if (GUI.Button(this.m_QuitButon, "Quit"))
                GUIManager.QuitEvent(this, null);
        }
    }
}
