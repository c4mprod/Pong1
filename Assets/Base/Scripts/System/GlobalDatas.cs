using UnityEngine;
using SerializerContract;
using SerializerFactory;
using SerializerException;
using System;

public class GlobalDatas : Singleton<GlobalDatas>
{
    #region "Datas"

    public PlayerDatas m_Player1 = new PlayerDatas();
    public PlayerDatas m_Player2 = new PlayerDatas();

    #endregion

    #region "Serializer"
    private ISerializerFactory m_Factory = null;
    private ISerializer m_Sr = null;
    #endregion

    #region "Data loads"
    public void LoadData<T>(ref T _Data, string _Path)
        where T : new()
    {
        try
        {
            Debug.LogError("UNSERIALIZE : " + Application.dataPath + _Path);
            this.m_Sr.Unserialize<T>(ref _Data, Application.dataPath + _Path);
        }
        catch (Exception e)
        {
            Debug.LogError("Cannot load : " + e.Message);
            if (_Data == null)
            {
                _Data = new T();
            }
        }
    }
    #endregion

    public void Initialize()
    {
    }

    public GlobalDatas()
    {
        this.m_Factory = new Factory();
        this.m_Sr = this.m_Factory.Create(SerializerType.XmlSerializer);

        this.Initialize();
    }
}
