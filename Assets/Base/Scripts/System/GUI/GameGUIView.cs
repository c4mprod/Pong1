using UnityEngine;
using System.Collections.Generic;

public class GameGUIView : MonoBehaviour
{
    #region "Events"

    public static event CustomEventHandler ContinueEvent;
    public static event CustomEventHandler QuitEvent;

    #endregion

    #region "GUI Rect"

    private Rect m_Player1Rect;
    private Rect m_Player2Rect;
    private Rect m_RoundRunTimerRect;
    private Rect m_ContinueButton;
    private Rect m_QuitButon;

    #endregion

    private string m_RoundEndMsg = "";
    private delegate void StatesFunctions();
    private Dictionary<GameController.State, StatesFunctions> m_StatesFunctionsDictionary = new Dictionary<GameController.State,StatesFunctions>();

    void Awake()
    {
        this.m_Player1Rect = new Rect(0, 0, 200, 200);
        this.m_Player2Rect = new Rect(Screen.width - 250, 0, 500, 500);
        this.m_RoundRunTimerRect = new Rect(Screen.width / 2 - 100, 0, 500, 500);
        this.m_ContinueButton = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 20);
        this.m_QuitButon = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 20);

        this.m_StatesFunctionsDictionary[GameController.State.RoundStart] = this.RoundStart;
        this.m_StatesFunctionsDictionary[GameController.State.Pause] = this.Pause;
        this.m_StatesFunctionsDictionary[GameController.State.RoundRun] = this.RoundRun;
        this.m_StatesFunctionsDictionary[GameController.State.RoundEnd] = this.RoundEnd;
    }

    private void RoundEnd(Object _Obj, System.EventArgs _EventArg)
    {
        GameController.WinnerVO lWinnerVO = (GameController.WinnerVO)_EventArg;

        // The player sent is the winner.
        this.m_RoundEndMsg = (lWinnerVO.m_EPlayer == GlobalDatasModel.EPlayer.Player1)
            ? "Player 1 Win" : "Player 2 Win";
    }

    #region "States Functions"

    private void RoundStart()
    {
        GUI.Label(this.m_RoundRunTimerRect, "<size=40> Start in : " + (int)(GameController.Instance.m_StartTimer + 1.0f) + "</size>");
    }

    private void RoundRun()
    {
        GUI.Label(this.m_Player1Rect, "<size=40> Score : " + GlobalDatasModel.Instance.m_Player1.m_Score + "</size>");
        GUI.Label(this.m_Player2Rect, "<size=40> Score : " + GlobalDatasModel.Instance.m_Player2.m_Score + "</size>");
        GUI.Label(this.m_RoundRunTimerRect, "<size=40> Time : " + GlobalDatasModel.Instance.m_LevelDatas.m_CurrentTime.FloatToTimeString() + "</size>");
    }

    private void RoundEnd()
    {
        GUI.Label(this.m_RoundRunTimerRect, "<size=40>" + this.m_RoundEndMsg + "</size>");
    }

    private void Pause()
    {
        this.RoundRun();
        if (GUI.Button(this.m_ContinueButton, "Continue"))
            GameGUIView.ContinueEvent(this, null);
        if (GUI.Button(this.m_QuitButon, "Quit"))
            GameGUIView.QuitEvent(this, null);
    }

    #endregion

    void OnEnable()
    {
        GameGUIView.ContinueEvent += GameController.Instance.OnContinue;
        GameGUIView.QuitEvent += GameController.Instance.OnQuit;
        GameController.RoundEndEvent += this.RoundEnd;
    }

    void OnDisable()
    {
        GameGUIView.ContinueEvent -= GameController.Instance.OnContinue;
        GameGUIView.QuitEvent -= GameController.Instance.OnQuit;
        GameController.RoundEndEvent -= this.RoundEnd;
    }

    void OnGUI()
    {
        if (this.m_StatesFunctionsDictionary.ContainsKey(GameController.Instance.m_CurrentState))
            this.m_StatesFunctionsDictionary[GameController.Instance.m_CurrentState]();
    }
}
