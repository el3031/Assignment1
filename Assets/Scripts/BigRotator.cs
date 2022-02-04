using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRotator : MonoBehaviour
{
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
    }
}
