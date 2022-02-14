using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 0.5f);
    }

    void OnTriggerEnter()
    {
        SceneManager.LoadScene(nextScene);
    }
}
