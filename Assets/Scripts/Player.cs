using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float delay;
    public float walkSpeed;
    public float slidingTime;

    private Vector3 mousePos;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        delay = slidingTime;
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        if(delay < slidingTime) {
            delay += Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && Vector2.Distance(transform.position, mousePos) > 0.6f) {
            Movement();

            if(Input.GetButton("Jump") && delay >= slidingTime) {
                delay = 0;
                StartCoroutine(Sliding());
            }
        }
    }

    private void Movement() {
        Vector2 dirVec = mousePos - transform.position;
        Vector2 nextVec = dirVec.normalized * walkSpeed * Time.fixedDeltaTime;

        float angle = Mathf.Atan2(nextVec.y, nextVec.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rb.SetRotation(rotation);

        rb.MovePosition(rb.position + nextVec);
    }

    IEnumerator Sliding() {
        walkSpeed *= 2;
        yield return new WaitForSeconds(0.3f);
        walkSpeed /= 2;
    }
}