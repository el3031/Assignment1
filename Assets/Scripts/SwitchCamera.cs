using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    int current;
    
    // Start is called before the first frame update
    void Start()
    {
        //make sure only 1 camera is active at a time
        current = 1;
        OnSwitchCamera();
    }

    public void OnSwitchCamera()
    {
        if (++current == cameras.Length)
        {
            current = 0;
        }
        //for all the cameras that are toggled off, set them inactive
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == current);
        } 
    }

    //slider triggers this function
    public void OnFOVChange(float value)
    {
        cameras[current].GetComponent<Camera>().fieldOfView = value;
    }
}
