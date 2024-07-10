using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string changeSceneName;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        SceneManager.LoadScene(changeSceneName);
    }
}
