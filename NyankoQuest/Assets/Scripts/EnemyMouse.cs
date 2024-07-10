using UnityEngine;

public class EnemyMouse : MonoBehaviour
{
    public float speed = 1.0f; // 移動速度
    public Transform leftBoundary; // 左の境界位置
    public Transform rightBoundary; // 右の境界位置
    public GameManager gameManager; // ゲームマネージャー
    private Rigidbody2D rb; // Rigidbody2Dコンポーネントの参照
    private bool movingLeft = true; // 現在の移動方向を示すフラグ
    private bool isFacingRight = true; // 現在の向きを示すフラグ

    // 初期化処理
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    // 毎フレームの更新処理
    void Update()
    {
        // クリボーの移動処理を呼び出し
        Move();
        // 移動方向のチェック処理を呼び出し
        CheckDirection();
    }

    // クリボーの移動処理
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

        // 向きを変更するかチェック
        if (movingLeft && isFacingRight)
        {
            Flip();
        }
        else if (!movingLeft && !isFacingRight)
        {
            Flip();
        }
    }

    // 移動方向のチェック処理
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

    // 向きを変更する処理
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;
    }

    // トリガー衝突時の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
    }
}
