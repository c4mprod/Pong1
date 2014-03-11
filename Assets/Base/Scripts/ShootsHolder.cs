using UnityEngine;
using System.Collections.Generic;

public class ShootsHolder : MonoBehaviour
{
    public GameObject m_PrefabShoot;
    public int m_MaxShoots = 50;

    private List<GameObject> m_ShootsList = null;
    private GameObjectPool m_ShootsPool = null;
    private GameObject m_Player = null;

    public void Initialize(GameObject _Player)
    {
        this.m_ShootsList = new List<GameObject>();
        this.m_ShootsPool = new GameObjectPool();
        this.m_ShootsPool.Generate(this.m_MaxShoots, this.m_PrefabShoot, this.transform);
        this.m_Player = _Player;
    }

    #region "Events functions"

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

    private void OnDisableShoot(Object _Obj, System.EventArgs _EventArg)
    {
        GameObject lShoot = (GameObject)_Obj;

        this.m_ShootsPool.PutObject(lShoot);
        this.m_ShootsList.Remove(lShoot);
    }

    #endregion
}
