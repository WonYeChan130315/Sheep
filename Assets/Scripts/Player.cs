using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    private Vector3 mousePos;
    private Rigidbody2D rb;

    private void Awake() {
        // 변수 초기화 하기
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // 마우스 위치 가져오기
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 마우스를 누르고, 마우스와의 위치가 0.6보다 멀면 실행
        if(Input.GetMouseButton(0) && Vector2.Distance(transform.position, mousePos) > 0.6f) {
            // 따라다닐 각도 구하기
            Vector2 direction = mousePos - transform.position;
            // 방향 정하기
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            // 이동하기
            transform.position += transform.up * walkSpeed * Time.deltaTime;
        }
    }
}