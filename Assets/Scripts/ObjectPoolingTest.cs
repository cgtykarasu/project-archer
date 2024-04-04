using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingTest : MonoBehaviour
{
    // Dictionary<Type, Queue<GameObject>> objectPool = new Dictionary<Type, Queue<GameObject>>();
    //
    // public GameObject GetObject(GameObject prefab)
    // {
    //     Type prefabType = prefab.GetType();
    //     
    //     if (objectPool.TryGetValue(prefabType, out Queue<GameObject> queue))
    //     {
    //         if (queue.Count > 0)
    //         {
    //             GameObject obj = queue.Dequeue();
    //             obj.SetActive(true);
    //             return obj;
    //         }
    //
    //         return CreateNewObject(prefab);
    //     }
    //
    //     return CreateNewObject(prefab);
    // }
    //
    // GameObject CreateNewObject(GameObject prefab)
    // {
    //     GameObject obj = Instantiate(prefab);
    //     obj.name = prefab.name;
    //     AddToPool(obj);
    //     return obj;
    // }
    //
    // void AddToPool(GameObject obj)
    // {
    //     Type type = obj.GetType();
    //     if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
    //     {
    //         queue.Enqueue(obj);
    //     }
    //     else
    //     {
    //         Queue<GameObject> newQueue = new Queue<GameObject>();
    //         newQueue.Enqueue(obj);
    //         objectPool.Add(type, newQueue);
    //     }
    //     obj.SetActive(false);
    // }
    //
    // public void ReturnObject(GameObject obj)
    // {
    //     Type type = obj.GetType();
    //     
    //     if (objectPool.TryGetValue(type, out Queue<GameObject> queue))
    //     {
    //         queue.Enqueue(obj);
    //     }
    //     else
    //     {
    //         Queue<GameObject> newQueue = new Queue<GameObject>();
    //         newQueue.Enqueue(obj);
    //         objectPool.Add(type, newQueue);
    //     }
    //     obj.SetActive(false);
    // }
}