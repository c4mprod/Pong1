using UnityEngine;
using System.Collections.Generic;

public class GenericPool<T>
    where T : Object
{
    #region "Exceptions"

    [System.Serializable]
    public class GenericPoolException : System.Exception
    {
        public GenericPoolException() { }
        public GenericPoolException(string message) : base(message) { }
        public GenericPoolException(string message, System.Exception inner) : base(message, inner) { }
        protected GenericPoolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    #endregion

    protected int m_Size = 0;
    protected List<T> m_ObjectsList;

    public GenericPool()
    {
        this.m_ObjectsList = new List<T>();
    }

    public virtual void Generate(int _GenerationNumber, T _ObjectToInstantiate)
    {
        int i = -1;

        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            this.m_ObjectsList.Add((T)Object.Instantiate(_ObjectToInstantiate));
        }
    }

    public virtual void PutObject(T _Object)
    {
        if (this.m_ObjectsList.Count < this.m_Size)
            this.m_ObjectsList.Add(_Object);
        else
            throw new GenericPoolException("The pool is full.");
    }

    public virtual T GetObject()
    {
        if (this.m_ObjectsList.Count > 0)
        {
            T lObject = this.m_ObjectsList[0];
            this.m_ObjectsList.RemoveAt(0);
            return lObject;
        }
        else
            throw new GenericPoolException("The pool is empty.");
    }
}
