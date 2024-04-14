using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootArrow : MonoBehaviour
{
    public float launchForce;
    public GameObject arrowPrefab;
    GameObject arrow;
    public float tensionIncreasePerSecond;

    IInstantiater<GameObject> instantiater;
    IArrowShooter arrowShooter;

    public float minSpeed = 1f;
    public float maxSpeed = 1000f;
    
    void Start()
    {
        instantiater = ServiceLocator.GetService<IInstantiater<GameObject>>();
        arrowShooter = ServiceLocator.GetService<IArrowShooter>();

        UniTaskAsyncEnumerable.EveryUpdate()
            .Where(_ => arrowShooter.Charging)
            .Subscribe(_ =>
            {
                launchForce += tensionIncreasePerSecond * Time.deltaTime;
                launchForce = Mathf.Clamp(launchForce, minSpeed, maxSpeed);
            })
            .AddTo(gameObject.GetCancellationTokenOnDestroy());

        UniTaskAsyncEnumerable.EveryUpdate()
            .Where(_ => arrowShooter.Shoot)
            .Subscribe(_ => { Shoot(); })
            .AddTo(gameObject.GetCancellationTokenOnDestroy());
    }
    
    void Shoot()
    {
        arrow = instantiater.Instantiate(arrowPrefab, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
        launchForce = minSpeed;
    }
}