using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaa : MonoBehaviour
{
    public GameObject crowTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            crowTrigger.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Enemy"))
    {
        crowTrigger.SetActive(false);
    }
}
}
