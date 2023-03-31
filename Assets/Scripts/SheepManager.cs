using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance;

    public List<GameObject> sheepList;
    public bool[] finishList;

    public Transform sheepGroup;
    public GameObject sheep;
    public GameObject reader;
    public int sheepCount;
    public int count;

    private void Awake() {
        instance = this;
        sheepCount = Random.Range(4, 7);
        
        finishList = new bool[sheepCount];
        for(int j = 0; j < sheepCount; j++) {
            GameObject clone = Instantiate(sheep, sheepGroup);
            sheepList.Add(clone);
        }

        SetReader();
    }

    private void SetReader() {
        int rand = Random.Range(0, sheepList.Count);
        sheepList[rand].name = "Reader";
        reader = sheepList[rand];
        reader.GetComponent<Rigidbody2D>().mass = 10;
    }
 
    public void Finish(GameObject gameObject) {
        finishList[count] = true;
        count++;
        UIManager.instance.count++;
        
        sheepList.Remove(gameObject);
        Destroy(gameObject);

        if(count < finishList.Length && gameObject.name == "Reader") {
            SetReader();
        } else if(count == finishList.Length) {
            Success();
        }
    }

    public void Fail() {
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene(0);
    }

    private void Success() {
        UIManager.instance.ScoreUp();
        SceneManager.LoadScene(0);
    }
}