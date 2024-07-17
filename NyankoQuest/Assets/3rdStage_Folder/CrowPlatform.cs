using UnityEngine;

public class CrowPlatform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private BoxCollider2D triggerCollider;

    void Start()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        if (colliders.Length != 2)
        {
            Debug.LogError("Platform should have exactly two BoxCollider2D components.");
            return;
        }

        boxCollider = colliders[0];
        triggerCollider = colliders[1];
        triggerCollider.isTrigger = true; // 2つ目のコライダーをトリガーに設定
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Crow has landed on the platform");
            // 必要な処理をここに追加
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // プレイヤーがトリガーに入った場合、プレイヤーとの衝突を無視する
            Physics2D.IgnoreCollision(collider, boxCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // プレイヤーがトリガーから出た場合、プレイヤーとの衝突を再び有効にする
            Physics2D.IgnoreCollision(collider, boxCollider, false);
        }
    }
}
