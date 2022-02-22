using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text TimeText;
    [SerializeField] private string nextScene;

    void Start()
    {
        //get the time and score from player prefs and set them
        float time = PlayerPrefs.GetFloat("time");
        int score = PlayerPrefs.GetInt("score");

        ScoreText.text = score.ToString();
        TimeText.text = ((int) Mathf.Floor(time)).ToString() + " seconds";
    }

    public void RestartGame()
    {
        //delete all player prefs (time and score)
        //then go back to scene1
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(nextScene);
    }
}
