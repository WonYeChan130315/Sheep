using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpDelay;
    public float walkSpeed;
    public RectTransform joystickRect;
    public RectTransform joystickHandleRect;

    [HideInInspector] public float jumpTime;

    private Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpTime = jumpDelay;
    }

    private void Update() {
        if(jumpTime < jumpDelay) {
            jumpTime += Time.deltaTime;
        }

        if(joystickHandleRect.anchoredPosition != Vector2.zero) {
            Vector2 direction = joystickHandleRect.anchoredPosition - joystickRect.anchoredPosition;

            Vector2 moveVec = direction * walkSpeed * Time.deltaTime;
            rigidbody.MovePosition(rigidbody.position + moveVec);

            if(moveVec.sqrMagnitude == 0)
                return;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 0, angle)));

            animator.SetBool("IsRun", true);
        } else {
            animator.SetBool("IsRun", false);
        }
    }

    public void Jump() {
        if(!animator.GetBool("IsRun") || jumpTime < jumpDelay)
            return;

        animator.SetBool("IsRun", false);
        StartCoroutine("Sliding");
        
        jumpTime = 0;
    }

    IEnumerator Sliding() {
        walkSpeed *= 2.2f;
        yield return new WaitForSeconds(0.3f);
        walkSpeed /= 2.2f;
    }
}