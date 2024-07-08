using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f;
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;
    public float diveDuration = 2f;
    public float diveHeight = 5f; // 3/4円の半径

    private Vector2 startPos;
    private bool movingRight = true;
    private float waitTime;
    private float timer;
    private bool isDiving = false;
    private Vector2 diveStartPos;
    private float diveTimer;
    private Vector2 diveEndPos;

    private void Start()
    {
        startPos = transform.position;
        SetRandomWaitTime();
    }

    private void Update()
    {
        if (!isDiving)
        {
            HorizontalMovement();
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                isDiving = true;
                timer = 0;
                diveStartPos = transform.position;
                diveTimer = 0;
            }
        }
        else
        {
            ThreeQuarterCircularDive();
        }
    }

    private void HorizontalMovement()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }

    private void ThreeQuarterCircularDive()
    {
        diveTimer += Time.deltaTime;
        float t = diveTimer / diveDuration;
        if (t >= 1f)
        {
            isDiving = false;
            transform.position = new Vector2(diveEndPos.x, diveStartPos.y); // 急降下開始時の y 座標に戻る
            startPos = diveEndPos; // 新しい左右移動の開始位置を設定
            SetRandomWaitTime();
            return;
        }

        float angle = 2.0f * Mathf.PI * t; // 3/4円の角度を計算 (0から3π/2)
        float x = diveStartPos.x + diveHeight * Mathf.Sin(angle);
        float y = diveStartPos.y - diveHeight * (1 - Mathf.Cos(angle));
        diveEndPos = new Vector2(x, y);
        transform.position = diveEndPos;
    }

    private void SetRandomWaitTime()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }
}
