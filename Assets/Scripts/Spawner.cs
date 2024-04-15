using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1f;
    float timeSinceLastSpawn = 0f;
    // [SerializeField] GameObject prefab;
    [SerializeField] GameObject[] prefabs;
    List<GameObject> prefabList01;
    List<GameObject> prefabList02;
    [SerializeField] float Xspeed = 4.5f;
    [SerializeField] float Yspeed = 5.5f;

    IInstantiater<GameObject> gameObjectPooler;

    public Vector3 center;
    public Vector3 size;

    void Start()
    {
        prefabList01 = new List<GameObject>(Enumerable.Repeat(prefabs[0], 2));
        prefabList01.AddRange(Enumerable.Repeat(prefabs[1], 2));
        ShuffleList(prefabList01);
        prefabList02 = new List<GameObject>(prefabList01);
        
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Retrieve the IGameObjectPooler<GameObject> service.
        gameObjectPooler = ServiceLocator.GetService<IInstantiater<GameObject>>();
        if (gameObjectPooler == null)
        {
            Debug.LogError("GameObjectPooler service not found.");
        }
    }

    void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Update()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn && gameObjectPooler != null)
        {
            // Use the object pooler to instantiate a new object.
            // GameObject go = gameObjectPooler.Instantiate(prefab, transform.position, transform.rotation);
            // GameObject go = gameObjectPooler.Instantiate(prefab, pos, Quaternion.identity);
            // GameObject go = gameObjectPooler.Instantiate(prefabListTest1[Random.Range(0, prefabListTest1.Count)], pos, Quaternion.identity);
            GameObject go = gameObjectPooler.Instantiate(SelectRandomElement(), pos, Quaternion.identity);

            if (go != null)
            {
                // Optional: Check if the spawned object has a Rigidbody2D to prevent errors.
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Randomly set the velocity of the Rigidbody2D.
                    // rb.velocity = new Vector2(Xspeed, Yspeed);
                    rb.AddForce(Physics2D.gravity * Random.Range(-.01f, -1.1f), ForceMode2D.Impulse);
                    rb.velocity = new Vector2(Random.Range(-Xspeed, Xspeed), Random.Range(Yspeed * .70f, Yspeed));
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
    
    GameObject SelectRandomElement()
    {
        if (prefabList02.Count > 0)
        {
            int randomIndex = Random.Range(0, prefabList02.Count);
            GameObject selectedElement = prefabList02[randomIndex];
            
            // Debug.Log(prefabList02.Count + " eleman kaldı, seçilen eleman: " + selectedElement.name);
            
            prefabList02.RemoveAt(randomIndex);

            return selectedElement;
        }
        else
        {
            // Debug.Log("Tüm elemanlar seçildi, yeni bir seçim yapmak için liste yeniden oluşturuluyor.");
            prefabList02 = new List<GameObject>(prefabList01);
            return SelectRandomElement();
        }
    }
}