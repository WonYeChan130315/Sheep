using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public List<bool> finishList;

    public static SheepManager instance;
    public GameObject reader;
    public GameObject player;
    public int finishCount;
    
    [SerializeField] private List<GameObject> sheepList;

    private void Awake() {
        instance = this;

        int rand = Random.Range(0, sheepList.Count);
        for(int i = 0; i < sheepList.Count; i++) {
            if(i == rand) {
                sheepList[i].name = "Reader";
                sheepList[i].layer = 8;
                sheepList[i].GetComponent<SpriteRenderer>().color = Color.blue;
                reader = sheepList[i];
            }
        }

        for(int j = 0; j < sheepList.Count; j++) {
            finishList.Add(false);
        }
    }

    public static GameObject Return(string name) {
        switch(name) {
            case "Player":
                return instance.player;
            case "Reader":
                return instance.reader;
            default:
                return null;
        }
    }

    public static void Finish() {
        instance.finishList[instance.finishCount] = true;
        instance.finishCount++;
    }
}
