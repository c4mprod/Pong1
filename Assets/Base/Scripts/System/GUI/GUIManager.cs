using UnityEngine;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    private Rect m_Player1Rect;
    private Rect m_Player2Rect;

    void Awake()
    {
        this.m_Player1Rect = new Rect(0, 0, 200, 200);
        this.m_Player2Rect = new Rect(Screen.width - 250, 0, 500, 500);
    }

    void Update()
    {
    }

    void OnGUI()
    {
        GUI.Label(this.m_Player1Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player1.m_Score + "</size>");
        GUI.Label(this.m_Player2Rect, "<size=40> Score : " + GlobalDatas.Instance.m_Player2.m_Score + "</size>");
    }

    #region "Events functions"

    public void OnGoal(Object _Obj, System.EventArgs _EventArg)
    {

    }

    #endregion
}
