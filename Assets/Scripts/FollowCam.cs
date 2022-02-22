using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset;

    void Start()
    {
        //figure out the initial offset between player and camera
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        //move the camera to smoothly follow the player
        Vector3 newPos = player.transform.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, newPos, 0.5f);
    }
}
