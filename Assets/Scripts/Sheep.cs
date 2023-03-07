using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    // private Rigidbody2D rb;
    // private bool finish = false;
    
    // private void Awake() {
    //     // 변수 초기화 하기
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // private void Update() {
    //     if(finish) // 도착하면 실행 X
    //         return;

    //     SheepManager sheepManager = SheepManager.instance;
    //     Transform player = sheepManager.player.transform;
    //     Transform reader = sheepManager.reader.transform;

    //     if(name == "Reader") { // 리더이면 실행
    //         if(Vector2.Distance(transform.position, player.position) < sheepManager.maxDistReader && Physics2D.CircleCast(transform.position, transform.localScale.x * 2.5f, Vector2.zero, 0, LayerMask.GetMask("Fence"))) { // 플레이어와의 거리가 maxDistReader 보다 가깝다면 실행
    //             Movement(player, -90);
    //         }
    //     } else { // 리더 아니면 실행
    //         if(Vector2.Distance(transform.position, reader.position) < 2f) {
    //             Movement(player, -90);
    //         }
            
    //         if(Vector2.Distance(transform.position, reader.position) > 1.4f) { // 리더와의 거리가 1.4 보다 멀다면 실행
    //             Movement(reader, 90);
    //         } else {
    //             Movement(transform, 90, false);
    //         }
    //     }
    // }

    // private void Movement(Transform target, int direction, bool rot = true) {
    //     Vector3 dirVec2 = -transform.up;
    //     if(rot) {    // 따라다닐 각도 구하기
    //         Vector2 dirVec = target.position - transform.position;
    //         // 방향 정하기
    //         float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
    //         transform.rotation = Quaternion.AngleAxis(angle - direction, Vector3.forward);

    //         dirVec2 = transform.up;
    //     }
    //     // 이동하기
    //     transform.position += dirVec2 * SheepManager.instance.walkSpeed * Time.deltaTime;
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("Finish")) { // 닿은 오브젝트의 테크가 "Finish"면 실행
    //         // SheepManager의 Finish 함수 호출
    //         SheepManager.Finish();

    //         // 움직이지 못하게 하기
    //         rb.isKinematic = true;
    //         // 끝내기
    //         finish = true;
    //     }
    // }














    //리더
        //울타리 피하기

    //부하
        //울타리 피하기


    public float walkSpeed;
    public float maxDist_P;
    public float maxDist_RP;
    public float miniDist_R;

    private Transform reader;
    private Transform player;
    private Rigidbody2D rb;
    private bool finish;

    private void Start() {
        reader = GameObject.Find("Reader").transform;
        player = GameObject.Find("Player").transform;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(finish)
            return;

        if(name == "Reader") {
            if(Vector2.Distance(transform.position, player.position) < maxDist_RP) {
                Movement(player, false);
            } else {
                Movement(player, true, false);
            }
        } else {
            if(Vector2.Distance(transform.position, player.position) < maxDist_P) {
                Movement(player, false);
            } else if(Vector2.Distance(transform.position, reader.position) > miniDist_R) {
                Movement(reader, true);
            }
        }
    }

    private void Movement(Transform target, bool front, bool movement = true) {
        Vector2 dirVec;
        if(front) dirVec = target.position - transform.position;
        else dirVec = transform.position - target.position;

        Vector2 nextVec = dirVec.normalized * walkSpeed * Time.fixedDeltaTime;

        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        rb.SetRotation(Quaternion.AngleAxis(angle - 90, Vector3.forward));

        if(movement) {
            rb.MovePosition(rb.position + nextVec);
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Finish")) {
            SheepManager.Finish();

            rb.isKinematic = true;
            finish = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Fence")) {
            SheepManager.Fail();
        }
    }
}