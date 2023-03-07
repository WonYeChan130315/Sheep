using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance;

    public List<GameObject> sheepList;
    public List<bool> finishList;

    [HideInInspector] public int count;

    private void Awake() {
        instance = this;
        SetReader();

        for(int j = 0; j < sheepList.Count; j++) {
            finishList.Add(false);
        }
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
            Time.timeScale = 0;
        }
    }

    public static void Fail() {
        print("fail");
        Time.timeScale = 0;
    }
}