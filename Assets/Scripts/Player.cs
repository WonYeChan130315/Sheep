using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    private Vector2 mousePos;
    private Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButton(0) && Vector2.Distance(transform.position, mousePos) > 0.6f) {
            float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
   	        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

            transform.Translate(Vector2.up * walkSpeed * Time.deltaTime);
        }
    }
}