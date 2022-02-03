using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded;
    [SerializeField] private float maxMove;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(Input.mousePosition);
            Ray onScreenPoint = Camera.main.ScreenPointToRay(mousePos);
            
            RaycastHit hit;
            if (Physics.Raycast(onScreenPoint.origin, onScreenPoint.direction, out hit) && grounded)
            {
                Vector3 loc = (hit.point == transform.position) ? Vector3.zero : hit.point;
                Debug.Log(hit.collider.gameObject.name);
                float jumpForce = Mathf.Sqrt(jumpHeight * -2 * Physics2D.gravity.y * gravityScale);
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);                
                Vector3 tryLoc = loc - transform.position;
                grounded = !grounded;
                rb.velocity += tryLoc * Time.deltaTime * maxMove;
            }

            Debug.DrawRay(onScreenPoint.origin, onScreenPoint.direction, Color.red, 10f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
