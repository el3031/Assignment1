using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(Input.mousePosition);
            Ray onScreenPoint = Camera.main.ScreenPointToRay(mousePos);
            Debug.Log(onScreenPoint);
            
            RaycastHit hit;
            if (Physics.Raycast(onScreenPoint.origin, onScreenPoint.direction, out hit))
            {
                Vector3 loc = hit.point;
                rb.MovePosition(transform.position + loc * Time.deltaTime * 10f);
                
            }
            
            /*
            Vector3 newPosition = transform.position + onScreenPoint.direction * 10f;
            rb.MovePosition(transform.position + newPosition * Time.deltaTime);
            */
            Debug.DrawRay(onScreenPoint.origin, onScreenPoint.direction * 10f, Color.red, 10f);
        }
    }
}
