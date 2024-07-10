using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; //Rigidbody2D
    private float horizontal; // 水平方向
    private bool isFacingRight = true; // 
    private Animator animator; // アニメーター
    public Transform groundCheck; // groundCheckの位置
    public LayerMask groundLayer; // グランドのレイヤー
    public float speed; // プレイヤーの移動速度
    public float jumpingPower; // プレイヤーのジャンプ
    public float enemyDieJump; // 敵を踏んだ時のジャンプ
    public float checkRadius; // groundCheck

    public string Run = "Run"; //


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
        Animation();
    }
    private void Animation() //水平方向に入力があるときアニメーションを実行する
    {
        if (horizontal != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void Jump(InputAction.CallbackContext context) //ジャンプする
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

    private void OnDrawGizmos() //グランドチェックの範囲を赤い線で表示する
    {
        // Gizmosの色を設定します。ここでは赤色に設定しています。
        Gizmos.color = Color.red;

        // groundCheckの位置に円を描画します。
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    private bool IsGrounded() //グランドに着いてるかチェックする
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void Flip() //反対を向いたときにアニメーションを反転する
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context) //移動処理
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
