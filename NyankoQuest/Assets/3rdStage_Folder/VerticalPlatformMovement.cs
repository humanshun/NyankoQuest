using UnityEngine;
using System.Collections;

public class VerticalPlatformMovement : MonoBehaviour
{
    public float moveDistance = 5f; // 足場が移動する距離
    public float moveSpeed = 2f; // 足場の移動速度
    public float pauseDuration = 1f; // 足場が停止する時間
    public bool startMovingUp = true; // 最初に上に移動するかどうか

    private Vector3 initialPosition;
    private bool movingUp;

    void Start()
    {
        initialPosition = transform.position;
        movingUp = startMovingUp;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            if (movingUp)
            {
                // 上に移動
                while (transform.position.y < initialPosition.y + moveDistance)
                {
                    transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                // 上の終点で停止
                transform.position = new Vector3(transform.position.x, initialPosition.y + moveDistance, transform.position.z);
                yield return new WaitForSeconds(pauseDuration);

                // 下に移動
                movingUp = false;
            }
            else
            {
                // 下に移動
                while (transform.position.y > initialPosition.y - moveDistance)
                {
                    transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                // 下の終点で停止
                transform.position = new Vector3(transform.position.x, initialPosition.y - moveDistance, transform.position.z);
                yield return new WaitForSeconds(pauseDuration);

                // 上に移動
                movingUp = true;
            }
        }
    }
}
