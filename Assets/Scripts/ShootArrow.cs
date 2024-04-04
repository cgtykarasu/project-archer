using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DefaultNamespace;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float LaunchForce;
    public GameObject arrowPrefab;
    GameObject arrow;
    public float tensionIncreasePerSecond;

    // IInstantiaterr<GameObject> instantiaterr = new GameObjectInstantiaterr();
    IInstantiater<GameObject> instantiater;
    IArrowShooter arrowShooter;

    public float minSpeed = 1f;
    public float maxSpeed = 1000f;
    // public float maxPressedTime = 2f;

    // float pressedTime = 0f;
    // bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        instantiater = ServiceLocator.GetService<IInstantiater<GameObject>>();
        arrowShooter = ServiceLocator.GetService<IArrowShooter>();

        UniTaskAsyncEnumerable.EveryUpdate()
            .Where(_ => arrowShooter.Charging)
            .Subscribe(_ =>
            {
                LaunchForce += tensionIncreasePerSecond * Time.deltaTime;
                LaunchForce = Mathf.Clamp(LaunchForce, minSpeed, maxSpeed);
            })
            .AddTo(gameObject.GetCancellationTokenOnDestroy());

        UniTaskAsyncEnumerable.EveryUpdate()
            .Where(_ => arrowShooter.Shoot)
            .Subscribe(_ => { Shoot(); })
            .AddTo(gameObject.GetCancellationTokenOnDestroy());
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
    //     {
    //         pressedTime = 0f;
    //         isShooting = true;
    //         arrowPrefab.SetActive(true);
    //     }
    //
    //     if (Input.GetKey(KeyCode.Space) && isShooting)
    //     {
    //         pressedTime += Time.deltaTime;
    //         LaunchForce = Mathf.Clamp(pressedTime * maxSpeed, minSpeed, maxSpeed);
    //     }
    //     
    //     
    //     
    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         pressedTime = 0f;
    //         isShooting = false;
    //         Shoot();
    //         // if (arrow != null)
    //         // {
    //         //     Destroy(arrow, 2f);
    //         // }
    //     }
    // }


    void Shoot()
    {
        // float speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp01(pressedTime / maxPressedTime));
        // arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        arrow = instantiater.Instantiate(arrowPrefab, transform.position, transform.rotation);

        // UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate).ContinueWith(() =>
        // {
        //     arrow.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
        // });
        
        arrow.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
        
        LaunchForce = minSpeed;

        // Vector2 moveDirection = arrow.GetComponent<Rigidbody2D>().velocity;
        // float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}