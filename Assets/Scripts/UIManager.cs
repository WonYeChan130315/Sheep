using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startGroup, gameObjectGroup;
    public RectTransform selectImage;

    private void Awake() {
        startGroup.SetActive(true);
        gameObjectGroup.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameStart() {
        startGroup.SetActive(false);
        Time.timeScale = 1;
    }
}