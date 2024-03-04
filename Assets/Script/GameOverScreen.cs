using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    public Text timerText;

    void Start()
    {

    }

    void Update()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        timerText.text = "Time: " + PlayerPrefs.GetFloat("Time").ToString("F0") + "s";
    }
}
