using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectReturnTest : MonoBehaviour
{
    // private ObjectPoolingTest objectPoolingTest;
    IInstantiaterr<GameObject> instantiaterr;


    private bool shouldReturn = false;

    void Start()
    {
        // objectPoolingTest = FindObjectOfType<ObjectPoolingTest>();
        instantiaterr = ServiceLocator.GetService<IInstantiaterr<GameObject>>();

    }

    void Update()
    {
        // if (objectPoolingTest != null && shouldReturn)
        // {
        //     objectPoolingTest.ReturnObject(gameObject);
        //     shouldReturn = false;
        // }
        
        if (instantiaterr != null && shouldReturn)
        {
            instantiaterr.Destroy(gameObject);
            shouldReturn = false;
        }
    }

    public void ReturnObject()
    {
        shouldReturn = true;
    }
    
    void OnBecameInvisible()
    {
        // objectPoolingTest.ReturnObject(gameObject);
        instantiaterr.Destroy(gameObject);
    }
}
