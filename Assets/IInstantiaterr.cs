using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public interface IInstantiaterr<T> where T : UnityEngine.Object
    {
        T Instantiate(T prefab, Vector3 position, Quaternion rotation);
        void Destroy(T Object);
    }

    public class GameObjectInstantiaterr : IInstantiaterr<GameObject>
    {
        // public GameObjectInstantiaterr(GameObject prefab, Vector3 position, Quaternion rotation)
        // {
        //     this.prefab = prefab;
        //     this.position = position;
        //     this.rotation = rotation;
        // }
        //
        // GameObject prefab;
        // Vector3 position;
        // Quaternion rotation;
        //

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(prefab, position, rotation);
        }

        public void Destroy(GameObject Object)
        {
            UnityEngine.Object.Destroy(Object);
        }
    }
    
    // ########################################################################-- OBJECT POOLING TEST --######################################################################################
    
    public class GameObjectPooler : IInstantiaterr<GameObject>
    {
        Dictionary<Type, Queue<GameObject>> objectPool = new Dictionary<Type, Queue<GameObject>>();

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject obj = GetObject(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public void Destroy(GameObject Object)
        {
            Object.SetActive(false);
        }
        
        public GameObject GetObject(GameObject prefab)
        {
            Type prefabType = prefab.GetType();
        
            if (objectPool.TryGetValue(prefabType, out Queue<GameObject> queue))
            {
                if (queue.Count > 0)
                {
                    GameObject obj = queue.Dequeue();
                    obj.SetActive(true);
                    return obj;
                }

                return CreateNewObject(prefab);
            }

            return CreateNewObject(prefab);
        }
        
        GameObject CreateNewObject(GameObject prefab)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.name = prefab.name;
            AddToPool(obj);
            return obj;
        }
        
        void AddToPool(GameObject obj)
        {
            Type type = obj.GetType();
            if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
            {
                queue.Enqueue(obj);
            }
            else
            {
                Queue<GameObject> newQueue = new Queue<GameObject>();
                newQueue.Enqueue(obj);
                objectPool.Add(type, newQueue);
            }
            obj.SetActive(false);
        }
        
        public void ReturnObject(GameObject obj)
        {
            Type type = obj.GetType();
        
            if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
            {
                queue.Enqueue(obj);
            }
            else
            {
                Queue<GameObject> newQueue = new Queue<GameObject>();
                newQueue.Enqueue(obj);
                objectPool.Add(type, newQueue);
            }
            obj.SetActive(false);
        }
    }
}