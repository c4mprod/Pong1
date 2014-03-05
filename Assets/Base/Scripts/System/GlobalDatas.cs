using UnityEngine;
using SerializerContract;
using SerializerFactory;
using SerializerException;
using System;

public class GlobalDatas : Singleton<GlobalDatas>
{
    #region "Datas"
    #endregion

    #region "Serializer"
    private ISerializerFactory _factory = null;
    private ISerializer _sr = null;
    #endregion

    #region "Datas Getters"
    #endregion

    #region "Data loads"
    public void loadData<T>(ref T data, string path)
        where T : new()
    {
        try
        {
            Debug.LogError("UNSERIALIZE : " + Application.dataPath + path);
            this._sr.Unserialize<T>(ref data, Application.dataPath + path);
        }
        catch (Exception e)
        {
            Debug.LogError("Cannot load : " + e.Message);
            if (data == null)
            {
                data = new T();
            }
        }
    }
    #endregion

    public void initialize()
    {
    }

    public GlobalDatas()
    {
        this._factory = new Factory();
        this._sr = this._factory.Create(SerializerType.XmlSerializer);
    }
}
