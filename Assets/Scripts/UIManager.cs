using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public Text scoreText;
    public GameObject startGroup, gameObjectGroup;
    public int score;
    public int maxScore;

    private void Awake() {
        startGroup.SetActive(true);
        gameObjectGroup.SetActive(true);
        Time.timeScale = 0;
        instance = this;

        score = PlayerPrefs.GetInt("score");
        maxScore = PlayerPrefs.GetInt("maxScore");
    }

    private void Update() {
        scoreText.text = score.ToString();
    }

    public void GameStart() {
        startGroup.SetActive(false);
        Time.timeScale = 1;
    }

    public void ScoreUp() {
        score++;
        PlayerPrefs.SetInt("score", score);

        if(score > maxScore)
            maxScore = score;

        PlayerPrefs.SetInt("maxScore", maxScore);
    }
}