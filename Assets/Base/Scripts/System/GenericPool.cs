// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-07-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-09-2014
// ***********************************************************************
// <copyright file="GenericPool.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class GenericPool.
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericPool<T>
    where T : Object
{
    #region "Exceptions"

    /// <summary>
    /// Class GenericPoolException.
    /// </summary>
    [System.Serializable]
    public class GenericPoolException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericPoolException"/> class.
        /// </summary>
        public GenericPoolException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GenericPoolException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericPoolException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public GenericPoolException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected GenericPoolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    #endregion

    /// <summary>
    /// The m_ size
    /// </summary>
    protected int m_Size = 0;
    /// <summary>
    /// The m_ objects list
    /// </summary>
    protected List<T> m_ObjectsList;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericPool{T}"/> class.
    /// </summary>
    public GenericPool()
    {
        this.m_ObjectsList = new List<T>();
    }

    /// <summary>
    /// Generates the specified _ generation number.
    /// </summary>
    /// <param name="_GenerationNumber">The _ generation number.</param>
    /// <param name="_ObjectToInstantiate">The _ object to instantiate.</param>
    public virtual void Generate(int _GenerationNumber, T _ObjectToInstantiate)
    {
        int i = -1;

        this.m_Size = _GenerationNumber;
        while (++i < this.m_Size)
        {
            this.m_ObjectsList.Add((T)Object.Instantiate(_ObjectToInstantiate));
        }
    }

    /// <summary>
    /// Puts the object.
    /// </summary>
    /// <param name="_Object">The _ object.</param>
    /// <exception cref="GenericPool`1.GenericPoolException">The pool is full.</exception>
    public virtual void PutObject(T _Object)
    {
        if (this.m_ObjectsList.Count < this.m_Size)
            this.m_ObjectsList.Add(_Object);
        else
            throw new GenericPoolException("The pool is full.");
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns>T.</returns>
    /// <exception cref="GenericPool`1.GenericPoolException">The pool is empty.</exception>
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
