using Interfaces;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.AddService<IInstantiater<GameObject>>(new GameObjectInstantiater());
        // ServiceLocator.AddService<IInstantiaterr<GameObject>>(new GameObjectPooler());
        ServiceLocator.AddService<IArrowShooter>(new KeyboardArrowShooter());
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Create()
    {
        new GameObject(nameof(GameManager)).AddComponent<GameManager>();
    }
}