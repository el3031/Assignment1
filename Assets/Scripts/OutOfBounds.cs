using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 startPos;
    private Quaternion startRot;
    
    //get the player's original position and rotation
    void Start()
    {
        startPos = player.position;
        startRot = player.rotation;
    }

    //once the player enters the trigger, return them to the original start
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = startPos;
            other.transform.rotation = startRot;
        }
    }
}
