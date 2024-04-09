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

// ################################################################################################################################

// public interface IInstantiater<T> where T : UnityEngine.Object
// {
//     GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
//     void Destroy(GameObject gameObject);
// }
//
// public class GameObjectInstantiater : IInstantiater<GameObject>
// {
//     Dictionary<GameObject, Queue<GameObject>> pool = new();
//
//     public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
//     {
//         if (!pool.TryGetValue(prefab, out Queue<GameObject> queue) || queue.Count == 0)
//         {
//             return CreateNewObject(prefab, position, rotation);
//         }
//
//         GameObject obj = queue.Dequeue();
//         obj.transform.position = position;
//         obj.transform.rotation = rotation;
//         obj.SetActive(true);
//         return obj;
//     }
//
//     public void Destroy(GameObject gameObject)
//     {
//         gameObject.SetActive(false);
//         GameObject prefab = gameObject.GetComponent<PoolableObject>().Prefab;
//
//         if (prefab == null)
//         {
//             Debug.LogWarning("Destroyed object does not have a PoolableObject component or its prefab is not assigned.");
//             UnityEngine.Object.Destroy(gameObject);
//             return;
//         }
//
//         if (!pool.ContainsKey(prefab))
//         {
//             pool[prefab] = new Queue<GameObject>();
//         }
//
//         pool[prefab].Enqueue(gameObject);
//     }
//     
//     GameObject CreateNewObject(GameObject prefab, Vector3 position, Quaternion rotation)
//     {
//         GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
//         PoolableObject poolableComponent = obj.AddComponent<PoolableObject>();
//         poolableComponent.Prefab = prefab;
//
//         return obj;
//     }
// }
//
// public class PoolableObject : MonoBehaviour
// {
//     public GameObject Prefab { get; set; }
// }

// ********************************************************************************************************************************
// ********************************************************************************************************************************

// public interface IInstantiater<T> where T : UnityEngine.Object
// {
//     GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
//     void Destroy(GameObject gameObject);
// }
//
// public class GameObjectInstantiater : IInstantiater<GameObject>
// {
//     List<GameObject> activeObjectsPool = new();
//     List<GameObject> inactiveObjectsPool = new();
//
//     public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
//     {
//         // GameObject obj = FindInactiveObject(prefab);
//         
//         GameObject o1 = GetInactiveObject(prefab);
//         
//         if (o1 == null)
//         {
//             // obj = CreateNewObject(prefab, position, rotation);
//             o1 = Object.Instantiate(prefab, position, rotation);
//             o1.AddComponent<PoolableObject>().Prefab = prefab;
//         }
//         else
//         {
//             o1.transform.position = position;
//             o1.transform.rotation = rotation;
//             o1.SetActive(true);
//         }
//
//         activeObjectsPool.Add(o1);
//         return o1;
//     }
//
//     public void Destroy(GameObject gameObject)
//     {
//         // gameObject.SetActive(false);
//         // activeObjectsPool.Add(gameObject);
//         if (activeObjectsPool.Remove(gameObject))
//         {
//             gameObject.SetActive(false);
//             inactiveObjectsPool.Add(gameObject);
//         }
//         else
//         {
//             Debug.LogWarning("GameObjectInstantiater: Attempted to destroy an object that is not managed as active.");
//         }
//     }
//
//     GameObject FindInactiveObject(GameObject prefab)
//     {
//         foreach (GameObject obj in activeObjectsPool)
//         {
//             if (!obj.activeInHierarchy && obj.GetComponent<PoolableObject>().Prefab == prefab)
//             {
//                 return obj;
//             }
//         }
//         return null;
//     }
//     
//     private GameObject GetInactiveObject(GameObject prefab)
//     {
//         for (int i = 0; i < inactiveObjectsPool.Count; i++)
//         {
//             GameObject obj = inactiveObjectsPool[i];
//             if (!obj.activeSelf && obj.GetComponent<PoolableObject>().Prefab == prefab)
//             {
//                 inactiveObjectsPool.RemoveAt(i);
//                 return obj;
//             }
//         }
//
//         return null;
//     }
//     
//     public void ClearUnusedObjects()
//     {
//         foreach (var obj in inactiveObjectsPool)
//         {
//             Object.Destroy(obj);
//         }
//         inactiveObjectsPool.Clear();
//     }
//
//     GameObject CreateNewObject(GameObject prefab, Vector3 position, Quaternion rotation)
//     {
//         GameObject obj = Object.Instantiate(prefab, position, rotation);
//         PoolableObject poolableComponent = obj.AddComponent<PoolableObject>();
//         poolableComponent.Prefab = prefab;
//         activeObjectsPool.Add(obj);
//         return obj;
//     }
// }
//
// public class PoolableObject : MonoBehaviour
// {
//     public GameObject Prefab { get; set; }
// }

// ********************************************************************************************************************************
// ********************************************************************************************************************************

public interface IInstantiater<T> where T : UnityEngine.Object
{
    T Instantiate(T prefab, Vector3 position, Quaternion rotation);
    void Destroy(T gameObject);
}

public class GameObjectInstantiater : IInstantiater<GameObject>
{
    List<GameObject> activeObjects = new List<GameObject>();
    List<GameObject> inactiveObjects = new List<GameObject>();

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetInactiveObject(prefab);

        if (obj == null)
        {
            obj = Object.Instantiate(prefab, position, rotation);
            obj.AddComponent<PoolableObject>().Prefab = prefab;
        }
        else
        {
            // Önceki nesnenin fiziksel özelliklerini kopyala
            Rigidbody2D previousRigidbody = obj.GetComponent<Rigidbody2D>();
            Rigidbody2D newRigidbody = obj.GetComponent<Rigidbody2D>();

            newRigidbody.velocity = previousRigidbody.velocity;
            newRigidbody.angularVelocity = previousRigidbody.angularVelocity;

            // Nesnenin konum ve rotasyonunu güncelle ve aktif hale getir
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.GetComponent<Rigidbody2D>().isKinematic = false;
            obj.SetActive(true);
            
            // // Önceki nesnenin hızını ve hareketini uygula
            // PoolableObject poolableObject = obj.GetComponent<PoolableObject>();
            // obj.GetComponent<Rigidbody2D>().velocity = poolableObject.Velocity;
        }

        activeObjects.Add(obj);
        return obj;
    }

    // public void Destroy(GameObject gameObject)
    // {
    //     if (activeObjects.Contains(gameObject))
    //     {
    //         gameObject.SetActive(false);
    //         inactiveObjects.Add(gameObject);
    //         // activeObjects.Remove(gameObject);
    //     }
    //     // if (activeObjects.Remove(gameObject))
    //     // {
    //     //     gameObject.SetActive(false);
    //     //     inactiveObjects.Add(gameObject);
    //     // }
    //     else
    //     {
    //         Debug.LogWarning("GameObjectInstantiater: Attempted to destroy an object that is not managed as active.");
    //         Object.Destroy(gameObject);
    //     }
    // }
    
    public void Destroy(GameObject gameObject)
    {
        if (activeObjects.Remove(gameObject))
        {
            gameObject.SetActive(false);
            inactiveObjects.Add(gameObject);
        }
        else
        {
            Debug.LogWarning("GameObjectInstantiater: Attempted to destroy an object that is not managed as active.");
        }
    }

    private GameObject GetInactiveObject(GameObject prefab)
    {
        for (int i = 0; i < inactiveObjects.Count; i++)
        {
            GameObject obj = inactiveObjects[i];
            if (!obj.activeSelf && obj.GetComponent<PoolableObject>().Prefab == prefab)
            {
                inactiveObjects.RemoveAt(i);
                return obj;
            }
        }

        return null;
    }

    // This method can be called to clear unused objects and free memory.
    public void ClearUnusedObjects()
    {
        foreach (var obj in inactiveObjects)
        {
            Object.Destroy(obj);
        }
        inactiveObjects.Clear();
    }
}

public class PoolableObject : MonoBehaviour
{
    public Vector2 Velocity { get; set; }
    public GameObject Prefab { get; set; }
}