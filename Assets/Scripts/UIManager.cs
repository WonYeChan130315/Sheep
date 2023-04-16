using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public Text scoreText, maxScoreText, countText, slidingText;
    public Player player;
    public Color color;
    public GameObject startGroup, gameObjectGroup;
    public int score, maxScore;
    [HideInInspector] public int count;

    private int maxCount;
    private float timer;

    private void Awake() {
        startGroup.SetActive(true);
        gameObjectGroup.SetActive(true);
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
        slidingText.text = string.Format("{0:0.0}s", player.delay);

        if(player.delay >= player.slidingTime) {
            slidingText.color = color;
        } else {
            slidingText.color = Color.gray;
        }
    }

    public void GameStart() {
        startGroup.SetActive(false);
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