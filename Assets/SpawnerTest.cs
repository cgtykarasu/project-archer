using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class SpawnerTest : MonoBehaviour
// {
//     [SerializeField] float timeToSpawn = 1f;
//     float timeSinceLastSpawn = 0f;
//     // ObjectPoolingTest objectPoolingTest;
//     [SerializeField] GameObject prefab;
//     
//     IGameObjectPooler<GameObject> gameObjectPooler;
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         gameObjectPooler = ServiceLocator.GetService<IGameObjectPooler<GameObject>>();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         timeSinceLastSpawn += Time.deltaTime;
//         if (timeSinceLastSpawn >= timeToSpawn)
//         {
//             GameObject go = gameObjectPooler.Instantiate(prefab, transform.position, transform.rotation);
//             
//             // GameObject go = objectPoolingTest.GetObject(prefab);
//             // go.transform.position = transform.position;
//             // go.transform.rotation = transform.rotation;
//             
//             go.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(.5f, 3.5f), Random.Range(3f, 5.5f));
//             timeSinceLastSpawn = 0f;
//         }
//     }
// }

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1f;
    float timeSinceLastSpawn = 0f;
    [SerializeField] GameObject prefab;

    IGameObjectPooler<GameObject> gameObjectPooler;

    void Start()
    {
        // Retrieve the IGameObjectPooler<GameObject> service.
        gameObjectPooler = ServiceLocator.GetService<IGameObjectPooler<GameObject>>();
        if (gameObjectPooler == null)
        {
            Debug.LogError("GameObjectPooler service not found.");
        }
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn && gameObjectPooler != null)
        {
            // Use the object pooler to instantiate a new object.
            GameObject go = gameObjectPooler.Instantiate(prefab, transform.position, transform.rotation);

            // Optional: Check if the spawned object has a Rigidbody2D to prevent errors.
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Randomly set the velocity of the Rigidbody2D.
                rb.velocity = new Vector2(Random.Range(.5f, 3.5f), Random.Range(3f, 5.5f));
            }
            else
            {
                Debug.LogWarning("Spawned object does not have a Rigidbody2D component.");
            }

            // Reset the spawn timer.
            timeSinceLastSpawn = 0f;
        }
    }
}