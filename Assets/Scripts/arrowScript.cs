using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D rb;

    bool hasHit = false;
    public GameObject explosionEffectPrefab;
    GameObject explosionInstantiate;

    IInstantiater<GameObject> explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = ServiceLocator.GetService<IInstantiater<GameObject>>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
            TrackMovement();
        }
    }

    void TrackMovement()
    {
        Vector2 moveDirection = rb.velocity;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if(col.gameObject.tag == "Enemy" && !hasHit)
        // {
        //     hasHit = true;
        //     Destroy(gameObject);
        // }

        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        if (col.gameObject.activeSelf)
        {
            ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(col.gameObject);
        }

        ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(col.gameObject);
        ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(gameObject);
        explosionInstantiate = explosion.Instantiate(explosionEffectPrefab, col.transform.position, col.transform.rotation);
        Instantiate(explosionEffectPrefab, col.transform.position, Quaternion.identity); // Patlama efektini oynat
        ResetPhysics();
        CameraShaker.Invoke();
        // ScoreManager.Instance.AddScore(1);
        IScorable scorableTarget = col.GetComponent<IScorable>();
        if (scorableTarget != null)
        {
            ScoreManager.Instance.AddScore(scorableTarget.ScoreValue);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // if(col.gameObject.tag == "Enemy" && !hasHit)
        // {
        //     hasHit = true;
        //     Destroy(gameObject);
        // }

        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        // await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);
        // ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(gameObject);
        // ResetPhysics();

        // Bekleme süresi dolunca, GameObject'i yok edin

        // ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(gameObject);
        // ResetPhysics();
    }

    void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            ServiceLocator.GetService<IInstantiater<GameObject>>().Destroy(gameObject);
        }
    }

    async void ResetPhysics()
    {
        await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
        rb.isKinematic = false;
        hasHit = false;
    }
}