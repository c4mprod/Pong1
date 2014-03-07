using UnityEngine;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    #region "GUI Rect"

    private Rect m_Player1Rect;
    private Rect m_Player2Rect;
    private Rect m_RoundRunTimerRect;

    #endregion

    private string m_RoundEndMsg = "";

    void Awake()
    {
        this.m_Player1Rect = new Rect(0, 0, 200, 200);
        this.m_Player2Rect = new Rect(Screen.width - 250, 0, 500, 500);
        this.m_RoundRunTimerRect = new Rect(Screen.width / 2 - 100, 0, 500, 500);
    }

    void OnEnable()
    {
        GameManager.RoundEndEvent += this.RoundEnd;
    }

    private void RoundEnd(Object _Obj, System.EventArgs _EventArg)
    {
        GameManager.WinnerVO lWinnerVO = (GameManager.WinnerVO)_EventArg;

        this.m_RoundEndMsg = (lWinnerVO.m_EPlayer == GameManager.EPlayer.Player1)
            ? "Player 1 Win" : "Player 2 Win";
    }

    void OnGUI()
    {
        switch (GameManager.Instance.m_CurrentState)
        {
            case GameManager.State.RoundStart:
                {
                    GUI.Label(this.m_RoundRunTimerRect, "<size=40> Start in : " + (int)GameManager.Instance.m_StartTimer + "</size>");
              
                    break;
                }

            case GameManager.State.RoundRun:
                {
                    GUI.Label(this.m_Player1Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player1.m_Score + "</size>");
                    GUI.Label(this.m_Player2Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player2.m_Score + "</size>");
                    GUI.Label(this.m_RoundRunTimerRect, "<size=40> Time : " + (int)GlobalDatas.Instance.m_LevelDatas.m_CurrentTime+ "</size>");
                  
                    break;
                }

            case GameManager.State.RoundEnd:
                {
                    GUI.Label(this.m_RoundRunTimerRect, "<size=40>" + this.m_RoundEndMsg + "</size>");
              
                    break;
                }

            default:
                break;
        }
    }

    #region "Events functions"

    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {

    }

    #endregion
}
