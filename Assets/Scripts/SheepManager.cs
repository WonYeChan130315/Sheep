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
        int rand = Random.Range(count, sheepList.Count);
        sheepList[rand].name = "Reader";
    }

    public static void Finish() {
        instance.finishList[instance.count] = true;
        instance.count++;

        if(instance.count < instance.finishList.Count) {
            instance.SetReader();
        } else {
            print("success");
            SceneManager.LoadScene(0);
        }
    }

    public static void Fail() {
        print("fail");
        SceneManager.LoadScene(0);
    }
}