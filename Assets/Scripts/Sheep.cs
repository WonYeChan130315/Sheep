using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool finish = false;
    private float applySpeed;
    
    private void Awake() {
        // 변수 초기화 하기
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // 도착하면 실행 안함
        if(finish)
            return;

        SheepManager sheepManager = GameObject.Find("SheepManager").GetComponent<SheepManager>();
        Transform player = sheepManager.player.transform;
        Transform reader = sheepManager.reader.transform;

        // 리더이면 실행
        if(name == "Reader") {
            if(Physics2D.CircleCast(transform.position, transform.localScale.x, transform.up, 2.5f, LayerMask.GetMask("Fence"))) {
                RaycastHit2D hitR = Physics2D.Raycast(transform.position, transform.right, 2.5f, LayerMask.GetMask("Fence"));
                RaycastHit2D hitL = Physics2D.Raycast(transform.position, -transform.right, 2.5f, LayerMask.GetMask("Fence"));
                if(hitR.collider != null) {
                    print("turn right");
                    transform.eulerAngles += new Vector3(0, 0, 90);
                } else if(hitL.collider != null) {
                    print("turn left");
                    transform.eulerAngles -= new Vector3(0, 0, 90);
                } else {
                    int rand = Random.Range(0, 2);
                    if(rand == 0 ) {
                        print("turn right");
                        transform.eulerAngles += new Vector3(0, 0, 90);
                    } else {
                        print("turn left");
                        transform.eulerAngles -= new Vector3(0, 0, 90);
                    }
                }
            // 플레이어와의 거리가 maxDistance 보다 가깝다면 실행
            } else if(Vector2.Distance(transform.position, player.position) < sheepManager.maxDistance) {
                // 도망칠 각도 구하기
                Vector2 direction = transform.position - player.position;
                //방향 정하기
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
                //이동하기
                transform.position += transform.up * applySpeed * Time.deltaTime;
            // 플레이어와의 거리가 maxDistance 보다 멀다면 실행
            }

            if(Vector2.Distance(transform.position, player.position) < 4)
                applySpeed = sheepManager.runSpeed;
            else
                applySpeed = sheepManager.walkSpeed;

            // 밀리지 않게 하기
            rb.isKinematic = Vector2.Distance(transform.position, player.position) > sheepManager.maxDistance;
        // 리더 아니면 실행
        } else {
            // 리더와의 거리가 1.3 보다 멀다면 실행
            if(Vector2.Distance(transform.position, reader.position) > 1.4f) {
                // 따라다닐 각도 구하기
                Vector2 direction = reader.position - transform.position;
                // 방향 정하기
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
                // 이동하기
                transform.position += transform.up * applySpeed * Time.deltaTime;
            }

            if(Vector2.Distance(transform.position, reader.position) > 4)
                applySpeed = sheepManager.runSpeed;
            else
                applySpeed = sheepManager.walkSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 닿은 오브젝트의 테크가 "Finish"면 실행
        if(other.CompareTag("Finish")) {
            // SheepManager의 Finish 함수 호출
            SheepManager.Finish();

            // 움직이지 못하게 하기
            rb.isKinematic = true;
            // 끝내기
            finish = true;
        }
    }
}