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

    //goes into new scene upon trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int score;
            float time;
            
            //some if statements to make this work for both player scripts
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
            
            //set player prefs to preserve score and time between scenes
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetFloat("time", time);

            OnSceneChange();
        }
    }

    public void OnSceneChange()
    {
        SceneManager.LoadScene(nextScene);
    }
}
