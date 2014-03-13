// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-07-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-11-2014
// ***********************************************************************
// <copyright file="ShootsHolder.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class ShootsHolder.
/// </summary>
public class ShootsHolder : MonoBehaviour
{
    /// <summary>
    /// The m_ prefab shoot
    /// </summary>
    public GameObject m_PrefabShoot;
    /// <summary>
    /// The m_ maximum shoots
    /// </summary>
    public int m_MaxShoots = 50;

    /// <summary>
    /// The m_ shoots list
    /// </summary>
    private List<GameObject> m_ShootsList = null;
    /// <summary>
    /// The m_ shoots pool
    /// </summary>
    private GameObjectPool m_ShootsPool = null;
    /// <summary>
    /// The m_ player
    /// </summary>
    private GameObject m_Player = null;

    /// <summary>
    /// Initializes the specified _ player.
    /// </summary>
    /// <param name="_Player">The _ player.</param>
    public void Initialize(GameObject _Player)
    {
        this.m_ShootsList = new List<GameObject>();
        this.m_ShootsPool = new GameObjectPool();
        this.m_ShootsPool.Generate(this.m_MaxShoots, this.m_PrefabShoot, this.transform);
        this.m_Player = _Player;
    }

    #region "Events functions"

    /// <summary>
    /// Shoots the specified _ player.
    /// </summary>
    /// <param name="_Player">The _ player.</param>
    public void Shoot(GlobalDatasModel.EPlayer _Player)
    {
        GameObject lShoot = this.m_ShootsPool.GetObject();

        lShoot.GetComponent<Shoot>().Initialize(this.transform.position, this.m_Player.GetComponent<PlayerController>().m_Player);
        if (_Player == GlobalDatasModel.EPlayer.Player2)
            lShoot.GetComponent<Shoot>().SetMoveDirection(-1);
        lShoot.GetComponent<Shoot>().SetVerticalPosition(this.m_Player.transform.position.y);
        lShoot.GetComponent<Shoot>().m_DisableEvent += this.OnDisableShoot;
        this.m_ShootsList.Add(lShoot);
    }

    /// <summary>
    /// Handles the <see cref="E:DisableShoot" /> event.
    /// </summary>
    /// <param name="_Obj">The _ object.</param>
    /// <param name="_EventArg">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OnDisableShoot(Object _Obj, System.EventArgs _EventArg)
    {
        GameObject lShoot = (GameObject)_Obj;

        this.m_ShootsPool.PutObject(lShoot);
        this.m_ShootsList.Remove(lShoot);
    }

    #endregion
}
