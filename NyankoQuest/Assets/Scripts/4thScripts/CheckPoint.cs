using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkPointObject;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveCheckPoint();
        }
    }
    void SaveCheckPoint()
    {
        Debug.Log("aaaa");
        PlayerPrefs.SetFloat("CheckPointX", checkPointObject.transform.position.x);
        PlayerPrefs.SetFloat("CheckPointY", checkPointObject.transform.position.x);
        PlayerPrefs.SetFloat("CheckPointZ", checkPointObject.transform.position.x);
        PlayerPrefs.Save();
    }
}
