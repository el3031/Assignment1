using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObstacles : MonoBehaviour
{
    [SerializeField] private float initial;
    [SerializeField] private float final;
    [SerializeField] private float rate;

    private float target;

    // Update is called once per frame
    void Update()
    {
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
