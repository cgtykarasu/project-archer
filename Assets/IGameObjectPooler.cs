using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


// public interface IGameObjectPooler<T> where T : UnityEngine.Object
// {
//     T Instantiate(T prefab, Vector3 position, Quaternion rotation);
//     void Destroy(T Object);
// }
//
// public class GameObjectPooler : IGameObjectPooler<GameObject>
// {
//     Dictionary<Type, Queue<GameObject>> objectPool = new Dictionary<Type, Queue<GameObject>>();
//
//     public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
//     {
//         GameObject obj = GetObject(prefab);
//         obj.transform.position = position;
//         obj.transform.rotation = rotation;
//         return obj;
//     }
//
//     public void Destroy(GameObject Object)
//     {
//         Object.SetActive(false);
//     }
//         
//     public GameObject GetObject(GameObject prefab)
//     {
//         Type prefabType = prefab.GetType();
//         
//         if (objectPool.TryGetValue(prefabType, out Queue<GameObject> queue))
//         {
//             if (queue.Count > 0)
//             {
//                 GameObject obj = queue.Dequeue();
//                 obj.SetActive(true);
//                 return obj;
//             }
//
//             return CreateNewObject(prefab);
//         }
//
//         return CreateNewObject(prefab);
//     }
//         
//     GameObject CreateNewObject(GameObject prefab)
//     {
//         GameObject obj = Object.Instantiate(prefab);
//         obj.name = prefab.name;
//         AddToPool(obj);
//         return obj;
//     }
//         
//     void AddToPool(GameObject obj)
//     {
//         Type type = obj.GetType();
//         if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
//         {
//             queue.Enqueue(obj);
//         }
//         else
//         {
//             Queue<GameObject> newQueue = new Queue<GameObject>();
//             newQueue.Enqueue(obj);
//             objectPool.Add(type, newQueue);
//         }
//         obj.SetActive(false);
//     }
//         
//     public void ReturnObject(GameObject obj)
//     {
//         Type type = obj.GetType();
//         
//         if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
//         {
//             queue.Enqueue(obj);
//         }
//         else
//         {
//             Queue<GameObject> newQueue = new Queue<GameObject>();
//             newQueue.Enqueue(obj);
//             objectPool.Add(type, newQueue);
//         }
//         obj.SetActive(false);
//     }
// }

public interface IGameObjectPooler<T> where T : UnityEngine.Object
{
    GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
    void Destroy(GameObject gameObject);
}

public class GameObjectPooler : IGameObjectPooler<GameObject>
{
    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!pool.TryGetValue(prefab, out Queue<GameObject> queue) || queue.Count == 0)
        {
            return CreateNewObject(prefab, position, rotation);
        }

        GameObject obj = queue.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
        GameObject prefab = gameObject.GetComponent<PoolableObject>().Prefab;

        if (prefab == null)
        {
            Debug.LogWarning("Destroyed object does not have a PoolableObject component or its prefab is not assigned.");
            UnityEngine.Object.Destroy(gameObject);
            return;
        }

        if (!pool.ContainsKey(prefab))
        {
            pool[prefab] = new Queue<GameObject>();
        }

        pool[prefab].Enqueue(gameObject);
    }
    
    private GameObject CreateNewObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
        PoolableObject poolableComponent = obj.AddComponent<PoolableObject>();
        poolableComponent.Prefab = prefab;

        return obj;
    }
}

public class PoolableObject : MonoBehaviour
{
    public GameObject Prefab { get; set; }
}