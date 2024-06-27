using UnityEngine;

public class RotatingCross : MonoBehaviour
//回転足場を回転させる
{
    public float rotationSpeed = 30f; // 回転速度
    public bool clockwise = true; // 時計回りに回転するかどうか

    void Update()
    {
        // 回転方向を決定する
        float direction = clockwise ? -1f : 1f;
        
        // オブジェクトを回転させる
        transform.Rotate(Vector3.forward, direction * rotationSpeed * Time.deltaTime);
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
