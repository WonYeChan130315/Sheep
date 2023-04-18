using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public Text scoreText, maxScoreText, countText;
    public Player player;
    public GameObject startGroup;
    public GameObject[] UIObject;
    public int score, maxScore;

    [HideInInspector] public int count;

    private int maxCount;
    private float timer;

    public void Init() {
        startGroup.SetActive(true);
        for(int i = 0; i < UIObject.Length; i++) {
            UIObject[i].SetActive(false);
        }

        instance = this;
        Time.timeScale = 0;
        maxCount = SheepManager.instance.sheepCount;

        score = PlayerPrefs.GetInt("score");
        maxScore = PlayerPrefs.GetInt("maxScore");
    }

    private void Update() {
        scoreText.text = score.ToString();
        maxScoreText.text = maxScore.ToString();
        countText.text = maxCount + " / " + count;
    }

    public void GameStart() {
        startGroup.SetActive(false);
        for(int i = 0; i < UIObject.Length; i++) {
            UIObject[i].SetActive(true);
        }

        Time.timeScale = 1;
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ScoreUp() {
        score++;
        PlayerPrefs.SetInt("score", score);

        maxScore = Mathf.Max(score, maxCount);
        PlayerPrefs.SetInt("maxScore", maxCount);
    }
}