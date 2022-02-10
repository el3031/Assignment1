using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScenes34 : MonoBehaviour
{
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJump()
    {
        rb.AddForce(10f * Vector3.up, ForceMode.Impulse);
    }
}
