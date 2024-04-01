using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowScript : MonoBehaviour
{
    
    public Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = new Vector2(transform.position.x, transform.position.y);
        direction = bowPos - mousePos;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampAngle = Mathf.Clamp(angle, -25, 80);
        transform.rotation = Quaternion.AngleAxis(clampAngle, Vector3.forward);
        // Debug.Log(mousePos);
        // FaceMouse();
    }
    
    // void FaceMouse()
    // {
    //     transform.right = direction;
    // }
    
}
