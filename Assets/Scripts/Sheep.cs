using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{   
    [SerializeField] private Transform player;
    [SerializeField] private float walkSpeed;

    private enum dirType {forward, back, right, left}
    private Rigidbody2D rigid;
    private bool isRay;
    private bool isReader = true;
    private bool isRunning = true;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(!isRunning)
            return;

        if(name == "Reader") {
            if(Vector2.Distance(transform.position, SheepManager.Return("Player").transform.position) < 7)
                Movement(SheepManager.Return("Player").transform, Direction(dirType.back));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 3f, LayerMask.GetMask("Sheep"));
            isRay = hit.collider != null;
            if(hit.collider != null){
                Sheep sheep = hit.collider.gameObject.GetComponent<Sheep>();
                sheep.Back();
            }
        } else {
            if(Vector2.Distance(transform.position, SheepManager.Return("Reader").transform.position) > 1.2f && !isRay && isReader)
                Movement(SheepManager.Return("Reader").transform, Direction(dirType.forward));

            if((Vector2.Distance(transform.position, SheepManager.Return("Player").transform.position) > 1.5f && !isRay) || !isReader)
                Movement(SheepManager.Return("Player").transform, Direction(dirType.back));
        }
    }

    public IEnumerator Back() {
        if(!isRunning)
            yield return null;
        
        Movement(SheepManager.Return("Reader").transform, Direction(dirType.left), 5);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 3f, LayerMask.GetMask("Sheep"));
        while (isRay && hit.collider != null)
            Movement(SheepManager.Return("Reader").transform, Direction(dirType.left), 5);

        hit.collider.gameObject.GetComponent<Sheep>().Back();
    }

    private void Movement(Transform target, int direction, float speed = 1) {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+direction, Vector3.forward);

        transform.Translate(Vector2.up * walkSpeed * Time.deltaTime * speed);
    }


    private int Direction(dirType type) {
        switch(type) {
            case dirType.forward:
                return -90;
            case dirType.back:
                return 90;
            case dirType.right:
                return 180;
            case dirType.left:
                return 0;
            default:
                return-90;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Finish")) {
            SheepManager.Finish();

            rigid.isKinematic = true;
            isRunning = false;

            if(name == "Reader")
                isReader = false;
        }
    }
}