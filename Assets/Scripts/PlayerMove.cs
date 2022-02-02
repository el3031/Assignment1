using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TouchSimulation.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on current position of the touch each frame
            Debug.Log("Touch Position : " + touch.position);
        }
        else
        {
            Debug.Log("No touch contacts");
        }
    }
}
