using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.transform.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, newPos, 0.5f);
    }
}
