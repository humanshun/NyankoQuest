using UnityEngine;

using System.Collections;

public class CrowMovement : MonoBehaviour
{
    public float speed = 5f; // カラスの左右移動速度
    public Transform leftBoundary; // 左の境界位置
    public Transform rightBoundary; // 右の境界位置
    public float jumpInterval = 5.0f; // ジャンプの間隔
    public float jumpForce = 10.0f; // ジャンプ力
    private Rigidbody2D rb;
    private bool movingLeft = true;
    private Animator anim;

    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
    }

    void Update()
    {
        Move();
        CheckDirection();
    }

    void Move()
    {
        // 移動方向に応じて速度を設定
        if (movingLeft)
        {
            // 左に移動
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            // 右に移動
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    void CheckDirection()
    {
        // 左に移動中で左の境界を超えた場合
        if (movingLeft && transform.position.x <= leftBoundary.position.x)
        {
            // 移動方向を右に変更
            movingLeft = false;
        }
        // 右に移動中で右の境界を超えた場合
        else if (!movingLeft && transform.position.x >= rightBoundary.position.x)
        {
            // 移動方向を左に変更
            movingLeft = true;
        }
    }
    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval); // ジャンプ間隔を待つ
            rb.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse); // ジャンプ力を追加
        }
    }
}
