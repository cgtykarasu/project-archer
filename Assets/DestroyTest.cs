using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    
    // IInstantiaterr<GameObject> instantiaterr = new GameObjectInstantiaterr();
    void OnBecameInvisible()
    {
        ServiceLocator.GetService<IInstantiaterr<GameObject>>().Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ServiceLocator.GetService<IInstantiaterr<GameObject>>().Destroy(gameObject);
    }
}