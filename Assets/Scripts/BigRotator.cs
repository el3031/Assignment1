using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRotator : MonoBehaviour
{
    private float rotationSpeed;
    [SerializeField] private float direction;
    [SerializeField] private Vector3 axis;
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //randomly specified rotation speed
        rotationSpeed = direction * Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis.normalized * rotationSpeed);
    }
}
