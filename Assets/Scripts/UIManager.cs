using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Level[] levels;

    public Level level;
    public GameObject levelGroup, startGroup, gameObjectGroup;
    public RectTransform selectImage;

    private Sheep reader;
    private Player player;

    private void Awake() {
        SetActive(false, true, false);
        Time.timeScale = 0;
    }

    public void OpenLevel() {
        SelectLevel(1);
        SetActive(true, false, false);
    }

    public void SelectLevel(int level) {
        this.level = levels[level];
        selectImage.position = new Vector3(selectImage.position.x, this.level.selectLevel.position.y);
    }

    public void GameStart() {
        SetActive(false, false, true);

        reader = GameObject.Find("Reader").GetComponent<Sheep>();
        player = GameObject.Find("Player").GetComponent<Player>();

        player.walkSpeed = level.playerSpeed;
        reader.walkSpeed = level.sheepSpeed;
        
        Time.timeScale = 1;
    }

    public void OpenHome() {
        SetActive(false, true, false);
    }

    private void SetActive(bool level, bool start, bool gameObject) {
        levelGroup.SetActive(level);
        startGroup.SetActive(start);
        gameObjectGroup.SetActive(gameObject);
    }
}

[System.Serializable]
public struct Level {
    public string name;
    public float sheepSpeed;
    public float playerSpeed;
    public RectTransform selectLevel;
}