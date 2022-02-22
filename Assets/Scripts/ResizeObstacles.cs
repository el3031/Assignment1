using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObstacles : MonoBehaviour
{
    [SerializeField] private float initial;
    [SerializeField] private float final;
    private float rate;

    private float target;

    void Start()
    {
        //set the rate randomly
        rate = Random.Range(0.2f, 0.5f);
    }    
    // Update is called once per frame
    void Update()
    {
        //once the scale has reached the target, make it scale the opposite way
        if (transform.localScale.x <= initial)
        {
            target = 1f;
        }
        else if (transform.localScale.x >= final)
        {
            target = -1f;
        }
        transform.localScale += target * (Vector3.right + Vector3.forward) * rate * Time.deltaTime;
    }
}
