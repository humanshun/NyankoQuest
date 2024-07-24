using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    public static GameManager Instance;

    private Vector2 checkpointPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
    }

    public Vector2 GetCheckpointPosition()
    {
        return checkpointPosition;
    }
    public void GameOver()
    {
        SceneManager.LoadScene(sceneName);
    }
}
