using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public Text scoreText, maxScoreText, countText;
    public Player player;
    public Color color;
    public GameObject startGroup, gameObjectGroup, countTxt, scoreTxt, maxScoreTxt, joystick, sliding;
    public int score, maxScore;
    [HideInInspector] public int count;

    private int maxCount;
    private float timer;

    private void Awake() {
        startGroup.SetActive(true);
        gameObjectGroup.SetActive(true);
        countTxt.SetActive(false);
        scoreTxt.SetActive(false);
        maxScoreTxt.SetActive(false);
        joystick.SetActive(false);
        sliding.SetActive(false);
        Time.timeScale = 0;

        instance = this;

        score = PlayerPrefs.GetInt("score");
        maxScore = PlayerPrefs.GetInt("maxScore");
    }

    private void Start() {
        maxCount = SheepManager.instance.sheepCount;
    }

    private void Update() {
        scoreText.text = score.ToString();
        maxScoreText.text = maxScore.ToString();
        countText.text = maxCount + " / " + count;
    }

    public void GameStart() {
        startGroup.SetActive(false);
        countTxt.SetActive(true);
        scoreTxt.SetActive(true);
        maxScoreTxt.SetActive(true);
        joystick.SetActive(true);
        sliding.SetActive(true);
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

        if(score > maxScore)
            maxScore = score;

        PlayerPrefs.SetInt("maxScore", maxScore);
    }
}