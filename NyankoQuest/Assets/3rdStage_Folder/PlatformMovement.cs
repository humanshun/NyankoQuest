using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
//左右移動する足場
{
    public float moveDistance = 5f; // 足場が移動する距離
    public float moveSpeed = 2f; // 足場の移動速度
    public float pauseDuration = 1f; // 足場が停止する時間
    public bool startMovingRight = true; // 最初に右に移動するかどうか

    private Vector3 initialPosition;
    private bool movingRight;

    void Start()
    {
        initialPosition = transform.position;
        movingRight = startMovingRight;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            if (movingRight)
            {
                // 右に移動
                while (transform.position.x < initialPosition.x + moveDistance)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                // 右の終点で停止
                transform.position = new Vector3(initialPosition.x + moveDistance, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(pauseDuration);

                // 左に移動
                movingRight = false;
            }
            else
            {
                // 左に移動
                while (transform.position.x > initialPosition.x - moveDistance)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                // 左の終点で停止
                transform.position = new Vector3(initialPosition.x - moveDistance, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(pauseDuration);

                // 右に移動
                movingRight = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
