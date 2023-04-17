using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{
    public float walkSpeed;
    public float maxPlayerDist;
    private GameObject player;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        player = GameObject.Find("Player");
    }

    private void Update() {
        if(name == "Reader") {
            Movement(player.transform, false);
        } else {
            if(SheepManager.instance.reader != null) {
                if(Vector2.Distance(transform.position, player.transform.position) < maxPlayerDist) {
                    Movement(player.transform, false);
                } else {
                    Movement(SheepManager.instance.reader.transform, true);
                }
            }
        }
    }


    private void Movement(Transform target, bool front) {
        Vector2 dirVec;
        if(front) dirVec = target.position - transform.position;
        else dirVec = transform.position - target.position;

        Vector2 nextVec = dirVec.normalized * walkSpeed * Time.deltaTime * 30;

        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        rb.SetRotation(Quaternion.AngleAxis(angle + 90, Vector3.forward));

        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Finish")) { 
            SheepManager.instance.Finish(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Fence")) {
            SheepManager.instance.Fail();
        }
    }
}