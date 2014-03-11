using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RacketSelectionGUIView : MonoBehaviour
{
    #region "Events"

    private event CustomEventHandler m_OnRacketSelectedEvent;

    #endregion

    #region "Rect positions"

    private Rect m_GlobalBox;
    private Rect m_Box1, m_Box2, m_Box3;
    private Rect m_NameLabel, m_WidthLabel, m_SpeedLabel;
    private Rect m_SelectRacketButton;

    #endregion

    private GlobalDatasModel.EPlayer m_CurrentPlayer = GlobalDatasModel.EPlayer.Player1;
    private int m_CurrentPos = 1;
    private int m_PreviousPos = 0;
    private int m_NextPos = 0;
    private float m_ActionTimer = 0f;
    private GameController.SelectedRacketVO m_RacketSelectionVO = new GameController.SelectedRacketVO();

    public float m_ActionTimerDelay = 0.5f;

    void Awake()
    {
        this.m_GlobalBox = new Rect(Screen.width / 2 - 600, Screen.height / 2 - 250, 1200, 500);
        this.m_Box1 = new Rect(Screen.width / 2 - 350, Screen.height / 2 - 100, 200, 200);
        this.m_Box2 = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        this.m_Box3 = new Rect(Screen.width / 2 + 150, Screen.height / 2 - 100, 200, 200);
        this.m_NameLabel = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 500, 100);
        this.m_WidthLabel = new Rect(Screen.width / 2 - 80, Screen.height / 2 + 130, 500, 100);
        this.m_SpeedLabel = new Rect(Screen.width / 2 - 80, Screen.height / 2 + 160, 500, 100);
        this.m_SelectRacketButton = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 300, 200, 100);
    }

    void OnEnable()
    {
        this.m_OnRacketSelectedEvent += GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent += this.OnPlayerSelectionChanged;
        GameController.LeftEvent += this.OnLeft;
        GameController.RightEvent += this.OnRight;
        GameController.ReturnEvent += this.OnReturn;
    }

    void OnDisable()
    {
        this.m_OnRacketSelectedEvent -= GameController.Instance.OnRacketSelected;

        GameController.PlayerSelectionChangedEvent -= this.OnPlayerSelectionChanged;
        GameController.LeftEvent -= this.OnLeft;
        GameController.RightEvent -= this.OnRight;
        GameController.ReturnEvent -= this.OnReturn;
    }

    void OnGUI()
    {
        this.m_CurrentPos = GeneralHelpers.CalculateCollectionPositions(out this.m_PreviousPos, out this.m_NextPos,
             this.m_CurrentPos, GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count);

        GUI.Box(this.m_GlobalBox, "<size=30>" + m_CurrentPlayer + " Selection" + "</size>");
        GUI.Box(this.m_Box1,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_PreviousPos].m_Sprite.texture);
        GUI.Box(this.m_Box2,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Sprite.texture);
        GUI.Box(this.m_Box3,
            GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_NextPos].m_Sprite.texture);

        GUI.Label(this.m_NameLabel, "<size=20>Racket Name : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Name + "</size>");
        GUI.Label(this.m_WidthLabel, "<size=20>Racket Width : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Width + "</size>");
        GUI.Label(this.m_SpeedLabel, "<size=20>Racket Speed : " + GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[this.m_CurrentPos].m_Speed + "</size>");

        if (GUI.Button(this.m_SelectRacketButton, "<size=20> Select Racket </size>"))
        {
            this.OnReturn(this, null);
        }
    }

    private IEnumerator ActionTimer()
    {
        this.m_ActionTimer = this.m_ActionTimerDelay;
        while (this.m_ActionTimer > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            this.m_ActionTimer -= Time.deltaTime;
        }
    }

    #region "Events functions"

    private void OnPlayerSelectionChanged(Object _Obj, System.EventArgs _EventArg)
    {
        GameController.RacketSelectionPlayerVO lVO = (GameController.RacketSelectionPlayerVO)_EventArg;

        this.m_CurrentPlayer = lVO.m_Player;
    }

    private void OnRight(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());
            this.m_CurrentPos += 1;
        }
    }

    private void OnLeft(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());
            this.m_CurrentPos -= 1;
        }
    }

    private void OnReturn(Object _Obj, System.EventArgs _EventArg)
    {
        if (this.m_ActionTimer <= 0.0f)
        {
            StartCoroutine(this.ActionTimer());

            this.m_RacketSelectionVO.m_RacketSelectedPos = this.m_CurrentPos;
            this.m_RacketSelectionVO.m_Player = this.m_CurrentPlayer;
            this.m_OnRacketSelectedEvent(this, this.m_RacketSelectionVO);
        }
    }

    #endregion
}
