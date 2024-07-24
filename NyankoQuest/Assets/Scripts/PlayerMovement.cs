using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator; // Rigidbody2D コンポーネント
    private Rigidbody2D rb; // Animator コンポーネント
    private AudioSource audioSource; // AudioSourceコンポーネント
    public AudioClip jumpAudio; // ジャンプ音
    public AudioClip EnemyDieSound; // 敵を倒した時の音
    private float horizontal; // 水平方向の移動量
    private bool isFacingRight = true; // キャラクターが右を向いているかどうか
    public Transform groundCheck; // 地面の判定に使用する Transform オブジェクト
    public LayerMask groundLayer; // 地面のレイヤーマスク
    public float speed; // プレイヤーの移動速度
    public float jumpingPower; // プレイヤーのジャンプ力
    public float enemyDieJump; // 敵を踏んだ時のジャンプ力
    public float checkRadius; // 地面判定の範囲
    public static float diecount; // 残機
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 水平方向の速度を設定し、垂直方向の速度は現在の速度を維持する
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // プレイヤーの向きが変わったら画像を反転する処理
        if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }

        Audio();

        Animation();
    }
    private void Animation()
    {
        // 地面についているか
        if (!IsGrounded())
        {
            animator.SetBool("Jump", true); // ついていなかったら
        }
        else
        {
            animator.SetBool("Jump", false); // ついていたら
            
            // 地面についていて、移動しているか
            if (horizontal != 0)
            {
                animator.SetBool("Run", true); // 移動していたら
            }
            else
            {
                animator.SetBool("Run", false); // 移動していなかったら
            }
        }
    }
    private void Audio()
    {
        // スペースキーが押されたときに音を再生
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            audioSource.PlayOneShot(jumpAudio);
        }
    }

    // ジャンプ操作のハンドラメソッド
    public void Jump(InputAction.CallbackContext context)
    {
        //ジャンプ操作が実行された（ボタンが押された）かつプレイヤーが地面にいる場合
        if (context.performed && IsGrounded())
        {
            // プレイヤーの垂直方向の速度をジャンプの力に設定する（ジャンプを実行）
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        // ジャンプ操作がキャンセルされた（ボタンが離された）かつプレイヤーが上昇中の場合
        if (context.canceled && rb.velocity.y > 0f)
        {
            // プレイヤーの上昇速度を半分にする（短くジャンプするため）
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
    private bool IsGrounded() // プレイヤーが地面に接触しているかどうか
    {
        // groundCheck.position を中心に checkRadius の範囲で
        // groundLayer レイヤーに属するコライダーがあるかをチェックする
        // 見つかった場合は true を返し、見つからなかった場合は false を返す
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void Flip() // Playerの向きを反転する処理
    {
        // 現在の向きを反転する
        isFacingRight = !isFacingRight;

        // プレイヤーのローカルスケールを取得
        Vector3 localScale = transform.localScale;

        // x 軸のスケールを反転させる
        localScale.x *= -1f;
        
        // 反転したスケールをプレイヤーに適用する
        transform.localScale = localScale;
    }

    // プレイヤーの移動入力
    public void Move(InputAction.CallbackContext context)
    {
        // コンテキストから水平方向の入力値を読み取り、horizontal 変数に格納する
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

            // 音を鳴らす
            audioSource.PlayOneShot(EnemyDieSound);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したゲームオブジェクトが "Enemy" タグを持っているか
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // プレイヤーのライフを減らすメソッドを呼び出す
            GameManager.Instance.LoseLife();

            // 現在のライフをデバッグログに出力する
            Debug.Log(GameManager.Instance.life);
        }
    }
}
