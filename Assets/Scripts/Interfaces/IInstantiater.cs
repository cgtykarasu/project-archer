using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
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
                // obj.SetActive(true);
            }
            else
            {
                Rigidbody2D previousRigidbody = obj.GetComponent<Rigidbody2D>();
                Rigidbody2D newRigidbody = obj.GetComponent<Rigidbody2D>();

                newRigidbody.velocity = previousRigidbody.velocity;
                newRigidbody.angularVelocity = previousRigidbody.angularVelocity;

                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.GetComponent<Rigidbody2D>().isKinematic = false;
                obj.SetActive(true);
            }

            activeObjects.Add(obj);
            return obj;
        }
    
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

        GameObject GetInactiveObject(GameObject prefab)
        {
            // for (int i = 0; i < inactiveObjects.Count; i++)
            // {
            //     GameObject obj = inactiveObjects[i];
            //     if (!obj || !obj.activeSelf) continue; // Eğer obje null ya da aktif değilse, döngünün bir sonraki adımına geç.
            //
            //     PoolableObject poolableObject = obj.GetComponent<PoolableObject>();
            //     if (poolableObject && poolableObject.Prefab == prefab)
            //     {
            //         inactiveObjects.RemoveAt(i);
            //         return obj;
            //     }
            // }
            //
            // return null;
            for (int i = 0; i < inactiveObjects.Count; i++)
            {
                GameObject obj = inactiveObjects[i];
                if (obj != null && !obj.activeSelf && obj.GetComponent<PoolableObject>().Prefab == prefab)
                {
                    inactiveObjects.RemoveAt(i);
                    return obj;
                }
            }

            return null;
            
        }
    }

    public class PoolableObject : MonoBehaviour
    {
        public Vector2 Velocity { get; set; }
        public GameObject Prefab { get; set; }
    }
}