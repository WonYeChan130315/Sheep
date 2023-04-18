using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpDelay;
    public float walkSpeed;
    public RectTransform joystickRect;
    public RectTransform joystickHandleRect;

    public float jumpTime;

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
            Vector2 dirVec = joystickHandleRect.anchoredPosition - joystickRect.anchoredPosition;
            Vector2 nextVec = dirVec.normalized * walkSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(nextVec.y, nextVec.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            rigidbody.SetRotation(rotation);
            rigidbody.MovePosition(rigidbody.position + nextVec);

            animator.SetBool("IsRun", true);

            if(Input.GetButtonDown("Jump"))
                Jump();
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