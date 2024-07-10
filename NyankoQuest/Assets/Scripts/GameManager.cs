using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    public void GameOver()
    {
        SceneManager.LoadScene(sceneName);
    }
}
