using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private PlayerMove player;
    [SerializeField] private PlayerMoveScenes34 player34;

    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 0.5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int score;
            float time;
            
            if (player != null)
            {
                score = player.score;
                time = player.time;
            }
            else
            {
                score = player34.score;
                time = player34.time;
            }
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetFloat("time", time);

            SceneManager.LoadScene(nextScene);
        }
    }
}
