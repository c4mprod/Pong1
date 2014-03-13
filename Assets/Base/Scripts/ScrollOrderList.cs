// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-12-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-13-2014
// ***********************************************************************
// <copyright file="ScrollOrderList.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class ScrollOrderList.
/// </summary>
public class ScrollOrderList : MonoBehaviour
{
    /// <summary>
    /// The m_ cam
    /// </summary>
    public Camera m_Cam = null;
    /// <summary>
    /// The m_ size
    /// </summary>
    public int m_Size = 5;
    /// <summary>
    /// The m_ object prefab
    /// </summary>
    public GameObject m_ObjectPrefab = null;
    /// <summary>
    /// The m_ padding
    /// </summary>
    public float m_Padding = 10.0f;
    /// <summary>
    /// The m_ start relative position to cam
    /// </summary>
    public float m_StartRelativePositionToCam = 0.0f;

    /// <summary>
    /// The m_ objects list data
    /// </summary>
    private List<GameObject> m_ObjectsListData = new List<GameObject>();

    /// <summary>
    /// The m_ current position data
    /// </summary>
    private int m_CurrentPositionData = 2;
    /// <summary>
    /// Gets the m_ current position.
    /// </summary>
    /// <value>The m_ current position.</value>
    public int m_CurrentPosition
    {
        get { return this.m_CurrentPositionData; }
        private set { this.m_CurrentPositionData = value; }
    }

    /// <summary>
    /// The m_ cam position
    /// </summary>
    private Vector3 m_CamPosition;
    /// <summary>
    /// The m_ save position
    /// </summary>
    private Vector2 m_SavePosition;

    /// <summary>
    /// Gets the m_ objects list.
    /// </summary>
    /// <value>The m_ objects list.</value>
    public List<GameObject> m_ObjectsList
    {
        get { return m_ObjectsListData; }
        private set { m_ObjectsListData = value; }
    }

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    void Awake()
    {
        this.m_CamPosition = this.m_Cam.transform.position;
        this.transform.position = this.m_CamPosition;
        if (m_ObjectPrefab != null)
        {
            int i = -1;

            while (++i < this.m_Size)
            {
                GameObject lObj = (GameObject)GameObject.Instantiate(this.m_ObjectPrefab);

                lObj.transform.position = this.transform.position;
                lObj.transform.parent = this.transform;
                this.m_SavePosition = lObj.transform.position;
                if (i < m_CurrentPositionData)
                {
                    if (i == 0)
                        this.m_SavePosition.x += this.m_StartRelativePositionToCam * this.m_CurrentPositionData;
                    else if (i > 0)
                        this.m_SavePosition.x = this.m_ObjectsListData[i - 1].transform.position.x + this.m_Padding;
                }
                else if (i > m_CurrentPositionData)
                {
                    this.m_SavePosition.x = this.m_ObjectsList[i - 1].transform.position.x + this.m_Padding;
                }
                lObj.transform.position = this.m_SavePosition;
                lObj.GetComponent<OrderedObject>().m_Position = i;
                lObj.GetComponent<OrderedObject>().LoadRacketDatas(GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[i]);
                this.m_ObjectsList.Add(lObj);
            }
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// Called when [saw object].
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool OnlySawObject(int id)
    {
        int i = -1;

        if (!this.m_ObjectsListData[id].GetComponent<SpriteRenderer>().IsVisibleFrom(this.m_Cam))
            return (false);
        while (++i < this.m_ObjectsListData.Count)
        {
            if (i != id && this.m_ObjectsListData[i].GetComponent<SpriteRenderer>().IsVisibleFrom(this.m_Cam))
                return (false);
        }
        return (true);
    }

    /// <summary>
    /// Cams the can move.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private bool CamCanMove(float x, float y)
    {
        if ((this.m_CurrentPositionData == 0 && x < 0 && this.OnlySawObject(0))
            || (this.m_CurrentPositionData == (GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count - 1)
            && x > 0
            && this.OnlySawObject(this.m_ObjectsListData.Count - 1)))
            return false;
        return true;
    }

    /// <summary>
    /// Finds the object identifier with position.
    /// </summary>
    /// <param name="_Pos">The _ position.</param>
    /// <returns>System.Int32.</returns>
    private int FindObjectIdWithPos(int _Pos)
    {
        int i = -1;

        while (++i < this.m_ObjectsListData.Count)
        {
            if (this.m_ObjectsListData[i].GetComponent<OrderedObject>().m_Position == _Pos)
                return (i);
        }
        Debug.Log("ASKED POS : " + _Pos + " | CURRENT POS : " + this.m_CurrentPositionData);
        return (-1);
    }

    /// <summary>
    /// Moves the cam.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public void MoveCam(float x, float y)
    {
        if (this.CamCanMove(x, y))
        {
            this.m_CamPosition.x += x  / 5.0f;
            this.m_Cam.transform.position = this.m_CamPosition;

            int i = -1;

            while (++i < this.m_ObjectsListData.Count)
            {
                OrderedObject lObj = this.m_ObjectsListData[i].GetComponent<OrderedObject>();

                 if (this.m_CurrentPositionData < (GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count - 1)
                    && Mathf.Abs(this.m_Cam.transform.position.x - this.m_ObjectsListData[this.FindObjectIdWithPos(this.m_CurrentPositionData)].transform.position.x)
                    > Mathf.Abs(this.m_Cam.transform.position.x - this.m_ObjectsListData[this.FindObjectIdWithPos(this.m_CurrentPositionData + 1)].transform.position.x))
                {
                    ++this.m_CurrentPositionData;
                }
                else if (this.m_CurrentPositionData > 0
                    && Mathf.Abs(this.m_Cam.transform.position.x - this.m_ObjectsListData[this.FindObjectIdWithPos(this.m_CurrentPositionData)].transform.position.x)
                    > Mathf.Abs(this.m_Cam.transform.position.x - this.m_ObjectsListData[this.FindObjectIdWithPos(this.m_CurrentPositionData - 1)].transform.position.x))
                {
                    --this.m_CurrentPositionData;
                }
                if (!this.m_ObjectsListData[i].GetComponent<SpriteRenderer>().IsVisibleFrom(this.m_Cam))
                {
                    if (i == 0
                        && !this.m_ObjectsListData[i + 1].GetComponent<SpriteRenderer>().IsVisibleFrom(this.m_Cam)
                        && (this.m_ObjectsListData[this.m_ObjectsListData.Count - 1].GetComponent<OrderedObject>().m_Position < (GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count - 1))
                        && this.m_CurrentPositionData < (GlobalDatasModel.Instance.m_RacketsData.m_RacketsList.Count - 1))
                    {
                        this.m_SavePosition.x = this.m_ObjectsListData[this.m_ObjectsListData.Count - 1].transform.position.x;
                        this.m_SavePosition.x += this.m_Padding;
                        this.m_ObjectsListData[i].transform.position = this.m_SavePosition;
                        lObj.m_Position = this.m_ObjectsListData[this.m_ObjectsListData.Count - 1].GetComponent<OrderedObject>().m_Position + 1;
                        lObj.LoadRacketDatas(GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[lObj.m_Position]);
                        this.m_ObjectsListData.MoveToBack<GameObject>(i);
                    }
                    else if (i == (this.m_ObjectsListData.Count - 1)
                        && !this.m_ObjectsListData[i - 1].GetComponent<SpriteRenderer>().IsVisibleFrom(this.m_Cam)
                        && lObj.m_Position > 0
                        && this.m_CurrentPositionData > 0
                        && this.m_ObjectsListData[0].GetComponent<OrderedObject>().m_Position > 0)
                    {
                        this.m_SavePosition.x = this.m_ObjectsListData[0].transform.position.x;
                        this.m_SavePosition.x -= this.m_Padding;
                        this.m_ObjectsListData[i].transform.position = this.m_SavePosition;
                        this.m_ObjectsListData.MoveToFront<GameObject>(i);
                        lObj.m_Position = this.m_ObjectsListData[1].GetComponent<OrderedObject>().m_Position - 1;
                        lObj.LoadRacketDatas(GlobalDatasModel.Instance.m_RacketsData.m_RacketsList[lObj.m_Position]);
                    }
                }
            }
        }
    }
}
