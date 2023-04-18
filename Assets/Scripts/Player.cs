using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float delay;
    public float walkSpeed;
    public float slidingTime;
    public RectTransform joy;
    public RectTransform joyTransform;

    private Animator ac;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<Animator>();
        delay = slidingTime;
    }

    private void Update() {
        if(delay < slidingTime) {
            delay += Time.deltaTime;
        }

        if(joy.anchoredPosition != Vector2.zero) {
            Vector2 dirVec = joy.anchoredPosition - joyTransform.anchoredPosition;
            Vector2 nextVec = dirVec.normalized * walkSpeed * Time.deltaTime * 30;

            float angle = Mathf.Atan2(nextVec.y, nextVec.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            rb.SetRotation(rotation);
            rb.MovePosition(rb.position + nextVec);

            ac.SetBool("IsRun", true);

            if(Input.GetButtonDown("Jump")) {
                Jump();
            }
        } else {
            ac.SetBool("IsRun", false);
        }
    }

    public void Jump() {
        if(!ac.GetBool("IsRun") || delay < slidingTime)
            return;

        ac.SetBool("IsRun", false);
        StartCoroutine("Sliding");
        delay = 0;
    }

    IEnumerator Sliding() {
        walkSpeed *= 2.2f;
        yield return new WaitForSeconds(0.3f);
        walkSpeed /= 2.2f;
    }
}