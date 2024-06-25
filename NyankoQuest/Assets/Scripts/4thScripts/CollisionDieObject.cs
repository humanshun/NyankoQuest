using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDieObject : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager = new GameManager();
        gameManager.GameOver();
    }
}
