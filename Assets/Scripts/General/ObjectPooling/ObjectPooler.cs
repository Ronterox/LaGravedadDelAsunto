using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General.ObjectPooling
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject ObjectToPool;
        public int AmountToPool;
        public bool ShouldExpand;

        public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
        {
            ObjectToPool = obj;
            AmountToPool = Mathf.Max(amt, 2);
            ShouldExpand = exp;
        }
    }

    [AddComponentMenu("Penguins Mafia/Object Pooler")]
    public class ObjectPooler : MonoBehaviour
    {
        public List<ObjectPoolItem> ItemsToPool;
        public string PoolName;

        private List<List<GameObject>> m_PooledObjectsList;
        private List<GameObject> m_PooledObjects;
        private List<int> m_Positions;
        private Transform m_PoolContent;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            m_PooledObjectsList = new List<List<GameObject>>();
            m_PooledObjects = new List<GameObject>();
            m_Positions = new List<int>();

            if (!string.IsNullOrEmpty(PoolName))
            {
                m_PoolContent = new GameObject(PoolName).transform;
                Scene scene = SceneManager.GetSceneByName("GameCommon");
                if (scene.IsValid()) SceneManager.MoveGameObjectToScene(m_PoolContent.gameObject, scene);
            }
            for (var i = 0; i < ItemsToPool.Count; i++) ObjectPoolItemToPooledObject(i);
        }

        /// <summary>
        /// Get pooled object
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject GetPooledObject(int index)
        {
            int curSize = m_PooledObjectsList[index].Count;
            for (int i = m_Positions[index] + 1; i < m_Positions[index] + m_PooledObjectsList[index].Count; i++)
            {
                if (m_PooledObjectsList[index][i % curSize].activeInHierarchy) continue;
                m_Positions[index] = i % curSize;
                return m_PooledObjectsList[index][i % curSize];
            }

            if (!ItemsToPool[index].ShouldExpand) return null;
            
            GameObject obj = Instantiate(ItemsToPool[index].ObjectToPool, string.IsNullOrEmpty(PoolName) ? transform : m_PoolContent);
            obj.SetActive(false);
            
            m_PooledObjectsList[index].Add(obj);
            
            return obj;
        }

        /// <summary>
        /// Get a list of all pooled objects
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<GameObject> GetAllPooledObjects(int index) => m_PooledObjectsList[index];

        /// <summary>
        /// Add new object to pooling
        /// </summary>
        /// <param name="GO"></param>
        /// <param name="amt"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int AddObject(GameObject GO, int amt = 3, bool exp = true)
        {
            var item = new ObjectPoolItem(GO, amt, exp);
            int currLen = ItemsToPool.Count;
            ItemsToPool.Add(item);
            ObjectPoolItemToPooledObject(currLen);
            return currLen;
        }

        /// <summary>
        /// Transform pooled item to pooled obj
        /// </summary>
        /// <param name="index"></param>
        private void ObjectPoolItemToPooledObject(int index)
        {
            ObjectPoolItem item = ItemsToPool[index];

            m_PooledObjects = new List<GameObject>();
            for (var i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = Instantiate(item.ObjectToPool, !string.IsNullOrEmpty(PoolName) ? transform : m_PoolContent);
                obj.SetActive(false);
                m_PooledObjects.Add(obj);
            }
            m_PooledObjectsList.Add(m_PooledObjects);
            m_Positions.Add(0);
        }
    }
}

