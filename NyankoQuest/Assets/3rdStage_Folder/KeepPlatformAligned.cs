using UnityEngine;
using System.Collections;

public class KeepPlatformAligned : MonoBehaviour
{
    public Transform rotatingCross; // 回転する骨組みのTransform

    void Update()
    {
        // 足場の回転を常に元に戻す
        transform.rotation = Quaternion.identity;

        // 足場のローカル回転を回転する骨組みに合わせる
        Vector3 crossRotation = rotatingCross.rotation.eulerAngles;
        transform.Rotate(0, 0, -crossRotation.z);
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
