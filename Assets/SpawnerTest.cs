using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1f;
    float timeSinceLastSpawn = 0f;
    // ObjectPoolingTest objectPoolingTest;
    [SerializeField] GameObject prefab;
    
    IInstantiaterr<GameObject> instantiaterForPooling;
    
    // Start is called before the first frame update
    void Start()
    {
        instantiaterForPooling = ServiceLocator.GetService<IInstantiaterr<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn)
        {
            GameObject go = instantiaterForPooling.Instantiate(prefab, transform.position, transform.rotation);
            
            // GameObject go = objectPoolingTest.GetObject(prefab);
            // go.transform.position = transform.position;
            // go.transform.rotation = transform.rotation;
            
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(.5f, 3.5f), Random.Range(3f, 5.5f));
            timeSinceLastSpawn = 0f;
        }
    }
}
