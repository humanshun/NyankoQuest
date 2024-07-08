using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float speed;
    public float jumpingPower;
    public float enemyDieJump;
    public float checkRadius;

    public string Run = "Run";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        // Animation();
    }
    // private void Animation()
    // {
    //     if (horizontal != 0)
    //     {
    //         animator.SetBool("Run", true);
    //     }
    //     else
    //     {
    //         animator.SetBool("Run", false);
    //     }
    // }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmosの色を設定します。ここでは赤色に設定しています。
        Gizmos.color = Color.red;

        // groundCheckの位置に円を描画します。
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトが敵である場合
        if (collision.CompareTag("Enemy"))
        {
            // ジャンプする
            rb.velocity = new Vector2(rb.velocity.x, enemyDieJump);
            Destroy(collision.gameObject);
        }
    }
}
