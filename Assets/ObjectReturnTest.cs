using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ObjectReturnTest : MonoBehaviour
// {
//     // private ObjectPoolingTest objectPoolingTest;
//     // IInstantiater<GameObject> _ınstantiater;
//     IGameObjectPooler<GameObject> gameObjectPooler;
//
//
//     bool shouldReturn = false;
//
//     void Start()
//     {
//         // objectPoolingTest = FindObjectOfType<ObjectPoolingTest>();
//         gameObjectPooler = ServiceLocator.GetService<IGameObjectPooler<GameObject>>();
//
//     }
//
//     void Update()
//     {
//         // if (objectPoolingTest != null && shouldReturn)
//         // {
//         //     objectPoolingTest.ReturnObject(gameObject);
//         //     shouldReturn = false;
//         // }
//         
//         if (gameObjectPooler != null && shouldReturn)
//         {
//             gameObjectPooler.Destroy(gameObject);
//             shouldReturn = false;
//         }
//     }
//
//     public void ReturnObject()
//     {
//         shouldReturn = true;
//     }
//     
//     void OnBecameInvisible()
//     {
//         // objectPoolingTest.ReturnObject(gameObject);
//         gameObjectPooler.Destroy(gameObject);
//     }
// }

public class ObjectReturnTest : MonoBehaviour
{
    // Define the interface variable to hold the reference to the pooler.
    private IGameObjectPooler<GameObject> gameObjectPooler;
    // This flag controls whether the object should be returned to the pool.
    private bool shouldReturn = false;

    void Start()
    {
        // Get the GameObjectPooler service at the start.
        gameObjectPooler = ServiceLocator.GetService<IGameObjectPooler<GameObject>>();
        // Ensure the pooler is found. If not, log an error for easier debugging.
        if (gameObjectPooler == null)
        {
            Debug.LogError("GameObjectPooler service not found.");
        }
    }

    void Update()
    {
        // If it's time to return the object to the pool, do so.
        if (gameObjectPooler != null && shouldReturn)
        {
            gameObjectPooler.Destroy(gameObject);
            shouldReturn = false; // Reset the flag.
        }
    }

    // Public method to trigger the return from outside this script.
    public void ReturnObject()
    {
        shouldReturn = true;
    }
    
    void OnBecameInvisible()
    {
        // Automatically return the object to the pool when it's no longer visible.
        // It's a good practice to check if gameObjectPooler is not null to avoid exceptions.
        if (gameObjectPooler != null)
        {
            gameObjectPooler.Destroy(gameObject);
        }
    }
}
