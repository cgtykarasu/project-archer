using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Interfaces;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float launchForce;
    public GameObject arrowPrefab;
    GameObject arrow;
    public float tensionIncreasePerSecond;
    
    [SerializeField] Transform atmaNoktasi; // Atma noktası olarak kullanılacak Transform objesi


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
    
    void OnEnable()
    {
        EventManager.GameOver += DisableShooting;
    }

    void OnDisable()
    {
        EventManager.GameOver -= DisableShooting;
    }

    void DisableShooting()
    {
        enabled = false; // Bu bileşeni devre dışı bırakarak ok atmayı durdur
    }
    
    void Shoot()
    {
        // EventManager.TriggerArrowShot();
        // arrow = instantiater.Instantiate(arrowPrefab, transform.position, transform.rotation);
        // arrow.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
        // launchForce = minSpeed;
        
        // Atılacak okun pozisyonunu ve rotasyonunu belirle (atma noktasında)
        Vector3 atmaPozisyonu = atmaNoktasi.position;
        Quaternion atmaRotasyonu = atmaNoktasi.rotation;

        // Oku Instantiate et ve atma noktasında oluştur
        GameObject ok = Instantiate(arrowPrefab, atmaPozisyonu, atmaRotasyonu);

        // Okun Rigidbody2D bileşenine kuvvet uygula (atılacak yönü belirler)
        // Rigidbody2D okRigidbody = ok.GetComponent<Rigidbody2D>();
        // okRigidbody.AddForce(atmaNoktasi.right * launchForce, ForceMode2D.Impulse);
        ok.GetComponent<Rigidbody2D>().AddForce(Vector2.right * launchForce, ForceMode2D.Impulse);
    }
}