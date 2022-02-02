using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float maxMove;

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
            
            RaycastHit hit;
            if (Physics.Raycast(onScreenPoint.origin, onScreenPoint.direction, out hit))
            {
                Vector3 loc = hit.point;
                Debug.DrawLine(transform.position, loc, Color.blue, 10f);
                Vector3 newLoc = transform.position + loc.normalized * maxMove;
                rb.MovePosition(newLoc);

                float angleDiff = Vector3.Angle(transform.position, newLoc);
                transform.Rotate(0f, angleDiff, 0f);
            }
            
            /*
            Vector3 newPosition = transform.position + onScreenPoint.direction * 10f;
            rb.MovePosition(transform.position + newPosition * Time.deltaTime);
            */
            Debug.DrawRay(onScreenPoint.origin, onScreenPoint.direction, Color.red, 10f);
        }
    }
}
