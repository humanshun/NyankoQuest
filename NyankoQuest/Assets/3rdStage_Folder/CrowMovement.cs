using UnityEngine;
using System.Collections;

public class CrowMovement : MonoBehaviour
{
    public GameManager gameManager; // ゲームマネージャー
    public float speed = 5f; // カラスの左右移動速度
    public Transform leftBoundary; // 左の境界位置
    public Transform rightBoundary; // 右の境界位置
    public float jumpInterval = 5.0f; // ジャンプの間隔
    public float jumpForce = 10.0f; // ジャンプ力
    private Rigidbody2D rb;
    private bool movingLeft = true;
    private bool isFacingRight = true;

    void Start()
    {
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
        if (movingLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (movingLeft && isFacingRight)
        {
            Flip();
        }
        else if (!movingLeft && !isFacingRight)
        {
            Flip();
        }
    }

    void CheckDirection()
    {
        if (movingLeft && transform.position.x <= leftBoundary.position.x)
        {
            movingLeft = false;
        }
        else if (!movingLeft && transform.position.x >= rightBoundary.position.x)
        {
            movingLeft = true;
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);
            rb.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
        }
    }
}
