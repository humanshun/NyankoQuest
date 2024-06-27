using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkPointObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 例えば、プレイヤーがトリガーに入った時だけチェックポイントを保存したい場合
        if (other.CompareTag("Player"))
        {
            SaveCheckPoint();
        }
    }

    void SaveCheckPoint()
    {
        Debug.Log("Checkpoint saved at position: " + checkPointObject.transform.position);
        PlayerPrefs.SetFloat("CheckPointX", checkPointObject.transform.position.x);
        PlayerPrefs.SetFloat("CheckPointY", checkPointObject.transform.position.y);
        PlayerPrefs.SetFloat("CheckPointZ", checkPointObject.transform.position.z);
        PlayerPrefs.Save();
    }
}
