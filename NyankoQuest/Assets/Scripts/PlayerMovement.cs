using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float horizontal;
    public float speed;
    public float jumpingPower;
    public float enemyDieJump;
    private bool isFacingRight = true;
    public float fallThreshold = -1f;  // 落下とみなす高さの閾値

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (transform.position.y < fallThreshold)  // プレイヤーが閾値以下に落ちたかチェック
        {
            Respawn();  // リスポーン処理を呼び出す
        }
    }

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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
    void Respawn()
    {
        float respawnX = PlayerPrefs.GetFloat("CheckPointX", 0);  // チェックポイントのX座標を読み込む
        float respawnY = PlayerPrefs.GetFloat("CheckPointY", 0);  // チェックポイントのY座標を読み込む
        float respawnZ = PlayerPrefs.GetFloat("CheckPointZ", 0);  // チェックポイントのZ座標を読み込む

        transform.position = new Vector3(respawnX, respawnY, respawnZ);  // プレイヤーをチェックポイントに移動

        Debug.Log("Respawned at checkpoint: " + transform.position);
    }
}
