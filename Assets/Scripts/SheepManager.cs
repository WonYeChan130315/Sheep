using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public List<bool> finishList;

    [SerializeField] private List<GameObject> sheepList;

    public static SheepManager instance;
    public GameObject reader;
    public GameObject player;
    public int count;
    public float runSpeed;
    public float walkSpeed;
    public float maxDistance;
    

    private void Awake() {
        instance = this;
        // SetReader 함수 호출
        SetReader();

        // sheepLis에 크기만큼 실행
        for(int j = 0; j < sheepList.Count; j++) {
            // finishList에 거짓 상태인 bool 변수 추가하기
            finishList.Add(false);
        }
    }

    // 리더 정하는 함수
    private void SetReader() {
        // count번째 변수부터 sheepList에 마지막 변수까지인 난수 만들기
        int rand = Random.Range(count, sheepList.Count);
        // sheepList에 rand번째 오브젝트에 이름을 "Reader"로 바꾸기
        sheepList[rand].name = "Reader";
        // sheepList에 rand번째 오브젝트에 레이어를 8로 바꾸기
        sheepList[rand].layer = 8;
        // reader 변수를 sheepList에 rand번째 오브젝트로 바꾸기
        reader = sheepList[rand];
    }

    // 양이 도착하면 실행하는 함수
    public static void Finish() {
        // finishList에 count번째 변수를 참으로 바꾸기
        instance.finishList[instance.count] = true;
        // count에 1 더하기
        instance.count++;

        // count가 finishList에 크기보다 크면 실행
        if(instance.count < instance.finishList.Count)
            // SetReader 함수 호출
            instance.SetReader();
    }
}