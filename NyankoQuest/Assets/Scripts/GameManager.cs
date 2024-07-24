using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string respawnSceneName;
    [SerializeField] private string GameOverSceneName;
    
    private static GameManager instance;
    public static int life = 3;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseLife()
    {
        life--;
        if (life <= 0)
        {
            life = 3;
            SceneManager.LoadScene(GameOverSceneName);
        }
        else
        {
            SceneManager.LoadScene(respawnSceneName);
        }
    }
}
