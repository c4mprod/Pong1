using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool : GenericPool<GameObject> 
{
    public override void Generate(int _GenerationNumber, GameObject _ObjectToInstantiate)
    {
        int i = -1;

        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            GameObject lObject = (GameObject)GameObject.Instantiate(_ObjectToInstantiate);

            lObject.SetActive(false);
            this.m_ObjectsList.Add(lObject);
        }
    }

    public void Generate(int _GenerationNumber, GameObject _ObjectToInstantiate,
        Transform _Parent = null)
    {  
        int i = -1;
        
        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            GameObject lObject = (GameObject)GameObject.Instantiate(_ObjectToInstantiate);

            lObject.SetActive(false);
            if (_Parent != null)
                lObject.transform.parent = _Parent;
            this.m_ObjectsList.Add(lObject);
        }
    }

    public override void PutObject(GameObject _Object)
    {
        _Object.SetActive(false);

        try
        {
            base.PutObject(_Object);
        }
        catch (GenericPoolException _Exception)
        {
            throw _Exception;
        }
    }

    public override GameObject GetObject()
    {
        try
        {
            GameObject lObject = (GameObject)base.GetObject();

            lObject.SetActive(true);
            return lObject;
        }
        catch (GenericPoolException _Exception)
        {
            throw _Exception;
        }
    }
}
