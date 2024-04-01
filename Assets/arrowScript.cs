using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    
    Rigidbody2D rb;
    bool hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
            trackMovement();
        }
    }
    
    void trackMovement()
    {
        Vector2 moveDirection = rb.velocity;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
    }
}