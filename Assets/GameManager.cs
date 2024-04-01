using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.AddService<IInstantiaterr<GameObject>>(new GameObjectInstantiaterr());
        // ServiceLocator.AddService<IInstantiaterr<GameObject>>(new GameObjectPooler());
        ServiceLocator.AddService<IArrowShooter>(new KeyboardArrowShooter());
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Create()
    {
        new GameObject(nameof(GameManager)).AddComponent<GameManager>();
    }
}