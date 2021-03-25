﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.ObjectPooling
{
    /// <summary>
    /// Base item config data
    /// </summary>
    /// <typeparam name="TPool"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TConfig"></typeparam>
    [System.Serializable]
    public class PoolConfig<TPool, TObject, TConfig>
        where TPool : ObjectPool<TPool, TObject, TConfig>
        where TConfig : PoolConfig<TPool, TObject, TConfig>
        where TObject : PoolObject<TPool, TObject, TConfig>, new()
    {
        public int InitialObjectCount;
        public GameObject Prefab;
        public string PoolName;

        [System.NonSerialized] public ObjectPool<TPool, TObject, TConfig> pool;
    }

    /// <summary>
    /// MonoBehaviour pool reference
    /// </summary>
    /// <typeparam name="TPool"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TConfig"></typeparam>
    public abstract class ObjectPool<TPool, TObject, TConfig> : MonoBehaviour
        where TPool : ObjectPool<TPool, TObject, TConfig>
        where TConfig : PoolConfig<TPool, TObject, TConfig>
        where TObject : PoolObject<TPool, TObject, TConfig>, new()
    {
        public GameObject prefab;
        public int initialPoolCount = 10;
        public float lifetime;

        [HideInInspector]
        public List<TObject> pool = new List<TObject>();
        public string PoolName;

        protected Transform m_PoolContent;

        protected virtual void Start()
        {
            m_PoolContent = new GameObject(PoolName).transform;

            for (var i = 0; i < initialPoolCount; i++) pool.Add(CreateNewPoolObject());
        }

        protected TObject CreateNewPoolObject()
        {
            var newPoolObject = new TObject { instance = Instantiate(prefab, m_PoolContent, true), inPool = true };
            newPoolObject.SetReferences(this as TPool);
            newPoolObject.Sleep();
            return newPoolObject;
        }

        public virtual TObject Pop()
        {
            foreach (TObject t in pool.Where(t => t.inPool))
            {
                t.inPool = false;
                t.WakeUp();
                return t;
            }

            TObject newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
            newPoolObject.inPool = false;
            newPoolObject.WakeUp();
            return newPoolObject;
        }

        public virtual void Push(TObject poolObject)
        {
            poolObject.inPool = true;
            poolObject.Sleep();
        }
    }

    /// <summary>
    /// Individual pool item
    /// </summary>
    /// <typeparam name="TPool"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TConfig"></typeparam>
    [System.Serializable]
    public abstract class PoolObject<TPool, TObject, TConfig>
        where TConfig : PoolConfig<TPool, TObject, TConfig>
        where TPool : ObjectPool<TPool, TObject, TConfig>
        where TObject : PoolObject<TPool, TObject, TConfig>, new()
    {
        public bool inPool;
        public GameObject instance;
        public TPool objectPool;

        public void SetReferences(TPool pool)
        {
            objectPool = pool;
            SetReferences();
        }

        protected virtual void SetReferences() { }

        /// <summary>
        /// Set position
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector3 position) => instance.transform.position = position;

        public virtual void WakeUp() => instance.SetActive(true);

        public virtual void Sleep() => instance.SetActive(false);

        public virtual void ReturnToPool() => objectPool.Push(this as TObject);
    }
}
