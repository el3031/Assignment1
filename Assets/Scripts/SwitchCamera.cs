using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    float current;
    
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    public void OnSwitchCamera()
    {
        if (++current == cameras.Length)
        {
            current = 0;
            Debug.Log("reset current");
        }
        Debug.Log(current);
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == current);
        } 
    }
}
