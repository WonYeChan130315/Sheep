using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float walkSpeed;

    private Vector3 mousePos;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        PosSet();
    }

    private void FixedUpdate() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Movement();
    }

    private void Movement() {
        if(Input.GetMouseButton(0) && Vector2.Distance(transform.position, mousePos) > 0.6f) {
            Vector2 dirVec = mousePos - transform.position;
            Vector2 nextVec = dirVec.normalized * walkSpeed * Time.fixedDeltaTime;

            float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            rb.SetRotation(Quaternion.Lerp(transform.rotation, rotation, 0.5f));

            rb.MovePosition(rb.position + nextVec);
        }
    }

    private void PosSet() {
        transform.position = TileGenerator.instance.finish.transform.position;
    }
}