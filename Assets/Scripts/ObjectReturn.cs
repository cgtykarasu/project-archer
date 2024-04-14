using Interfaces;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    IInstantiater<GameObject> gameObjectPooler;
    bool shouldReturn = false;

    void Start()
    {
        gameObjectPooler = ServiceLocator.GetService<IInstantiater<GameObject>>();
        if (gameObjectPooler == null)
        {
            Debug.LogError("GameObjectPooler service not found.");
        }
    }

    void Update()
    {
        if (gameObjectPooler != null && shouldReturn)
        {
            gameObjectPooler.Destroy(gameObject);
            shouldReturn = false;
        }
    }

    public void ReturnObject()
    {
        shouldReturn = true;
    }
    
    void OnBecameInvisible()
    {
        if (gameObjectPooler != null && gameObject.activeSelf)
        {
            gameObjectPooler.Destroy(gameObject);
            // Debug.Log("OBJECT RETURN TARAFINDAN YOK EDİLDİ");
        }
    }
}