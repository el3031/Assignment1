using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRotator : MonoBehaviour
{
    public float rotationSpeed;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = direction * Random.Range(0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
    }
}
