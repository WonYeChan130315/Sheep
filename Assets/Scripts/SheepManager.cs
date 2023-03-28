using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance;

    public List<GameObject> sheepList;
    public List<bool> finishList;

    public Transform sheepGroup;
    public GameObject sheep;
    public GameObject reader;
    public int sheepCount;
    [HideInInspector] public int count;

    private void Awake() {
        instance = this;
        
        for(int j = 0; j < sheepCount; j++) {
            GameObject clone = Instantiate(sheep, sheepGroup);
            sheepList.Add(clone);
            finishList.Add(false);
        }

        SetReader();
    }

    private void SetReader() {
        int rand = Random.Range(0, sheepList.Count);
        sheepList[rand].name = "Reader";
        reader = sheepList[rand];
        reader.GetComponent<Rigidbody2D>().mass = 10;
    }

    public static void Finish(GameObject gameObject) {
        instance.finishList[instance.count] = true;
        instance.count++;
        
        instance.sheepList.Remove(gameObject);
        Destroy(gameObject);

        if(instance.count < instance.finishList.Count && gameObject.name == "Reader") {
            instance.SetReader();
        } else {
            print("성공");
            UIManager.instance.ScoreUp();
            SceneManager.LoadScene(0);
        }
    }

    public static void Fail() {
        print("실패");
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene(0);
    }
}